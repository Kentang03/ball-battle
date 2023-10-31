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
    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material gray;

    private float waypointTolerance = 2f;
    private float reactivateTime = 2.5f;

    private GameObject midPoint, bluePoint, redPoint;    
    private Mover mover;
    private GameObject target, goal, ball;
    private Vector3 randomPosition;

    private bool isArrive = true;
    [SerializeField] private bool isStunned = false;
    [SerializeField] private bool isDie = false;


    void Start() {
        mover = GetComponent<Mover>();
        ball = GameObject.FindWithTag("Ball");

        midPoint = GameObject.Find("mid_waypoint");
        redPoint = GameObject.Find("red_waypoint");
        bluePoint = GameObject.Find("blue_waypoint");

        SetGoalSide();
        SetColorSide();
    }

    void Update()
    {
        if (isDie)
        {
            Cancel();
            Destroy(this.gameObject);
            return;
        }

        if (isStunned)
        {
            StartCoroutine(ReactivateStun());
            return;
        } 
            
        if (!IsBallCarried()) {
            MoveTo(ball.transform.position, 1f);
        }

        else if (!IsPlayerCarrying())
        {
            isArrive = false;
            MoveTo(randomPosition, 1f);

        }

        else if (IsPlayerCarrying() && IsBallCarried())
        {
            MoveTo(goal.transform.position, 0.75f);
        }

        if (AtDestination(randomPosition))
        {
            isArrive = true;
            GetRandomDestination();
        }

        if (target == null) return;
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
    
    private void MoveTo(Vector3 target, float speedFraction)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        mover.StartMoveAction(target, speedFraction);
    }

    public void Cancel()
    {
        target = null;
        mover.Cancel();
    }

    private bool IsPlayerCarrying()
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
            this.gameObject.GetComponent<MeshRenderer>().material = blue;
        }
        else if (this.side == TeamSide.Red)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = red;
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

    public void Stunned()
    {
        isStunned = true;
    }
    
    public void Dead()
    {
        isDie = true;
    }

    IEnumerator ReactivateStun()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = gray;
        Cancel();
        yield return new WaitForSeconds(reactivateTime);
        SetColorSide();
        isStunned = false;
    }

}
