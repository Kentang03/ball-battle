using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI : MonoBehaviour, IAction
{
    [Header("Team Side")]
    [SerializeField] TeamSide side;
    
    [Header("Set Material")]
    [SerializeField] private Material redTeamMat;
    [SerializeField] private Material blueTeamMat;
    [SerializeField] private Material inactiveMat;
    
    [SerializeField] private GameObject field;

    bool isStandby = true;
    float detectionRange = 0.35f;
    float detectionRangeByField;
    float reactivateTime = 4f;
    float spawnTime = 0.5f;
    float normalSpeed = 1f;
    float returnSpeed = 2f;

    Vector3 size;
    Vector3 returnPosition;
    Mover mover;

    bool isActive = true;
    bool isAttackerInRange = false;
    [SerializeField] bool isCollision = false;
    bool isSpawned = false;

    [SerializeField] GameObject[] targets;
    AttackerAI target;

    void OnValidate() {
        SetColorSide();
    }

    void Start() {
        mover = GetComponent<Mover>();
        targets = GameObject.FindGameObjectsWithTag("Attacker");
        field = GameObject.Find("Field");

        size = field.GetComponent<MeshRenderer>().bounds.size;
        detectionRangeByField = (detectionRange * size.x) / 2;

        returnPosition = this.transform.position;
    }

    // When activated:
    // Standby after activated
    //      Chasing when the attacker with the Ball reaches the Detection circle:
    //      Lock the target to that attacker, and chase it at a speed (5)
    //      If caught the target, becomes Inactivated for a period of time (4)

    void Update()
    {
        if (!isSpawned)
        {
            StartCoroutine(Spawning());
            return;
        }

        if (!isActive)
        {
            StartCoroutine(Reactivate());
            return;
        } 

        targets = GameObject.FindGameObjectsWithTag("Attacker");
        // Get Attacker that carrying ball
        GetAttackerCarryingBall();

        if (target != null && TargetInDistance())
        {
            MoveTo(target.transform.position, normalSpeed);
            if (TargetInCollision() && target.IsActive())
            {
                this.Deactivate();
                target.Deactivate();
            }
        }

        else if (target == null)
        {
            MoveTo(returnPosition, returnSpeed);
        }


    }

    private bool TargetInDistance()
    {
        return Vector3.Distance(transform.position, target.transform.position) < detectionRangeByField;
    }
    
    private bool TargetInCollision()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= 1.25f;
    }

    private void GetAttackerCarryingBall()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<AttackerAI>().IsCarryingBall())
            {
                target = targets[i].GetComponent<AttackerAI>();
            }

            else if (targets[i].GetComponent<AttackerAI>().IsCarryingBall())
            {
                target = null;
            }
        }
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

    public void Deactivate()
    {
        isActive = false;
    }

    IEnumerator Spawning()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = inactiveMat;
        yield return new WaitForSeconds(spawnTime);
        SetColorSide();
        isSpawned = true;
    }

    IEnumerator Reactivate()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = inactiveMat;
        Cancel();
        mover.MoveTo(returnPosition, returnSpeed);
        yield return new WaitForSeconds(reactivateTime);
        SetColorSide();
        isActive = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRangeByField);
    }


}
