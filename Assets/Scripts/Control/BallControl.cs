using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [Header("Team Side")]
    [SerializeField] TeamSide side;
    [SerializeField] bool isCarried;
    [SerializeField] float force;
    [SerializeField] BallPoint ballPoint;
    public GameObject midPoint;
    public GameObject bluePoint;
    public GameObject redPoint;
    Vector3 randomPosition;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        isCarried = false; 
        GetRandomDestination();
        transform.position = randomPosition;
    }

    void Update() {
        if (ballPoint == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            isCarried = false;
            rb.AddForce(transform.rotation * Vector3.forward * force, ForceMode.Impulse);
        }

        if (isCarried)
        {
            this.transform.position = ballPoint.transform.position;
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

    private void GetRandomPosition(GameObject a, GameObject b)
    {
        randomPosition = new Vector3(GetRandom(a.transform.position.x, b.transform.position.x),
                                    GetRandom(a.transform.position.y, b.transform.position.y),
                                    GetRandom(a.transform.position.z, b.transform.position.z));

    }

    private float GetRandom(float a, float b)
    {
        return UnityEngine.Random.Range(a, b);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Attacker")
        {
            ballPoint = other.transform.GetComponentInChildren<BallPoint>();
            isCarried = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Attacker")
        {
            ballPoint = null;
            isCarried = false;
        }
    }

    public void ResetCarry() {
        ballPoint = null;
        isCarried = false;
    }

    public void Shoot(Quaternion target)
    {
        rb.AddForce(target * Vector3.forward * force, ForceMode.Impulse);
    }

    public bool IsCarried()
    {
        return isCarried;
    }

    public BallPoint GetBallPoint()
    {
        return ballPoint;
    }

    
}
