using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TeamSide
{
    Blue,
    Red
}

public class AttackerAI : MonoBehaviour, IAction
{
    [Header("Team Side")]
    [SerializeField] TeamSide side;
    [Header("Set Material")]
    [SerializeField] private Material redTeamMat;
    [SerializeField] private Material blueTeamMat;
    [SerializeField] private Material inactiveMat;

    private float waypointTolerance = 2f;
    private float reactivateTime = 2.5f;
    private float spawnTime = 0.5f;
    private float carryingSpeed = 0.75f;
    private float normalSpeed = 1.5f;
    private float passSpeed = 2.5f;

    private GameObject midPoint, bluePoint, redPoint;    
    private Mover mover;
    private GameObject goal, ball; 
    [SerializeField] private GameObject aura;
    private Vector3 randomPosition;

    AttackerAI[] targets;
    [SerializeField] AttackerAI nearestAlly;

    private bool isSpawned = false;
    private bool isArrive = true;
    private bool isActive = true;
    private bool isDie = false;

    private Quaternion passDirection;

    void OnValidate() {
        SetColorSide();
    }


    void Start() 
    {
        mover = GetComponent<Mover>();
        ball = GameObject.FindWithTag("Ball");

        midPoint = GameObject.Find("mid_waypoint");
        redPoint = GameObject.Find("red_waypoint");
        bluePoint = GameObject.Find("blue_waypoint");

        SetGoalSide();
    }

    void Update()
    {
        targets = FindObjectsOfType<AttackerAI>();
        GetClosestAlly();

        if (!isSpawned) 
        {
            StartCoroutine(Spawning());
            return;
        }
        if (!isActive)
        {
            if (IsCarryingBall())
            {
                ball.GetComponent<BallControl>().ResetCarry();
            }
            StartCoroutine(Reactivate());
            return;
        }

        if (isDie)
        {
            Cancel();
            ball.GetComponent<BallControl>().ResetCarry();
            Destroy(this.gameObject);
        }

            
        if (!IsBallCarried())
        {
            aura.gameObject.SetActive(false);
            MoveTo(ball.transform.position, normalSpeed);
        }

        else if (!IsCarryingBall())
        {
            isArrive = false;
            aura.gameObject.SetActive(false);
            MoveTo(randomPosition, normalSpeed);

        }

        else if (IsCarryingBall() && IsBallCarried())
        {
            aura.gameObject.SetActive(true);
            MoveTo(goal.transform.position, carryingSpeed);
            GetClosestAlly();
        }

        if (AtDestination(randomPosition))
        {
            isArrive = true;
            GetRandomDestination();
        }
    }


    private void GetRandomDestination()
    {
        if (this.side == TeamSide.Blue){
            GetRandomPosition(midPoint, bluePoint);
        }

        else if (this.side == TeamSide.Red){
            GetRandomPosition(midPoint, redPoint);
        }

    }

    private Vector3 GetRandomPosition(GameObject a, GameObject b){
        randomPosition = new Vector3(GetRandom(a.transform.position.x, b.transform.position.x),
                                     GetRandom(a.transform.position.y, b.transform.position.y),
                                     GetRandom(a.transform.position.z, b.transform.position.z));

        return randomPosition;
    }

    private float GetRandom(float a, float b)
    {
        return UnityEngine.Random.Range(a, b);
    }

    private Quaternion SetShootTarget()
    {
        Vector3 direction = (nearestAlly.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        return lookRotation;
    }

    
    private void MoveTo(Vector3 target, float speedFraction)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        mover.StartMoveAction(target, speedFraction);
    }

    public void Cancel()
    {
        mover.Cancel();
    }

    public bool IsCarryingBall()
    {
        return this.GetComponentInChildren<BallPoint>() == ball.GetComponent<BallControl>().GetBallPoint();
    }

    private bool IsBallCarried()
    {
        return ball.GetComponent<BallControl>().IsCarried();
    }

    void SetGoalSide()
    {
        if (this.side == TeamSide.Blue)
        {
            goal = GameObject.Find("Red_Goal");
        }
        else if (this.side == TeamSide.Red)
        {
            goal = GameObject.Find("Blue_Goal");
        }
    }
    
    void SetColorSide()
    {
        if (this.side == TeamSide.Blue)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = blueTeamMat;
        }
        else if (this.side == TeamSide.Red)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = redTeamMat;
        }
    }

    private bool AtDestination(Vector3 destination)
    {
        float distanceToDestination = Vector3.Distance(transform.position, destination);
        return distanceToDestination < waypointTolerance;
    }

    public TeamSide GetSide()
    {
        return side;
    }

    public void Deactivate()
    {
        isActive = false;
        
        // ball.GetComponent<BallControl>().SetBallRotation(passDirection);
        ball.GetComponent<BallControl>().Shoot(SetShootTarget());
        ball.GetComponent<BallControl>().ResetCarry();
    }

    private void GetClosestAlly()
    {
        float nearestDistance = 99999;

        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector3.Distance(this.transform.position, targets[i].transform.position);
            if (this == targets[i].GetComponent<AttackerAI>()) continue;
            if (distance < nearestDistance)
            {
                nearestAlly = targets[i].GetComponent<AttackerAI>();
            }
        }
    }

    public void Dead()
    {
        isDie = true;
    }

    public bool IsActive()
    {
        return isActive;
    }

    IEnumerator Reactivate()
    {
        aura.gameObject.SetActive(false);
        // this.GetComponent<NavMeshAgent>().radius = 0.1f;
        this.gameObject.GetComponent<MeshRenderer>().material = inactiveMat;
        Cancel();
        yield return new WaitForSeconds(reactivateTime);
        SetColorSide();
        // this.GetComponent<NavMeshAgent>().radius = 0.5f;
        isActive = true;
    }
    
    IEnumerator Spawning()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = inactiveMat;
        yield return new WaitForSeconds(spawnTime);
        SetColorSide();
        isSpawned = true;
    }
}
