using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum TeamSide
{
    Blue,
    Red
}

public class AttackerAI : MonoBehaviour, IAction
{
    [Header("Team Side")]
    [SerializeField] TeamSide side;
    GameObject midPoint, bluePoint, redPoint;    
    private Mover mover;
    private GameObject target;
    private GameObject goal;
    private GameObject ball;

    Vector3 randomPosition;

    bool isArrive = true;
    bool isGoal = false;

    float waypointTolerance = 2f;

    void Start() {
        mover = GetComponent<Mover>();
        ball = GameObject.FindWithTag("Ball");

        midPoint = GameObject.Find("mid_waypoint");
        redPoint = GameObject.Find("red_waypoint");
        bluePoint = GameObject.Find("blue_waypoint");

        GetGoalSide();
    }

    void Update()
    {
        if (!IsBallCarried()) {
            MoveTo(ball.transform.position, 0.75f);
        }

        else if (IsBallCarried() && !CompareBallPoint())
        {
            isArrive = false;
            MoveTo(randomPosition, 1f); 
        }

        else if (IsBallCarried() && CompareBallPoint())
        {
            MoveTo(goal.transform.position, 1f);
        }


        if (AtDestination(randomPosition))
        {
            isArrive = true;
            Debug.Log("Arrived" + this.name);
        }

        if (isArrive)
        {
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
        mover.StartMoveAction(target, 1f);
    }

    public void Cancel()
    {
        target = null;
        mover.Cancel();
    }

    private bool CompareBallPoint()
    {
        return this.GetComponentInChildren<BallPoint>() == ball.GetComponent<BallControl>().GetBallPoint();
    }

    private bool IsBallCarried()
    {
        return ball.GetComponent<BallControl>().IsCarried();
    }

    GameObject GetGoalSide()
    {
        if (this.side == TeamSide.Blue)
        {
            goal = GameObject.Find("Red_Goal");
        }
        else if (this.side == TeamSide.Red)
        {
            goal = GameObject.Find("Blue_Goal");
        }
        return goal;
    }

    private bool AtDestination(Vector3 destination)
    {
        float distanceToDestination = Vector3.Distance(transform.position, destination);
        return distanceToDestination < waypointTolerance;
    }


}
