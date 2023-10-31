using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] bool isCarried;
    [SerializeField] float force;
    [SerializeField] BallPoint ballPoint;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        isCarried = false; 
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
            this.transform.rotation = ballPoint.transform.rotation;
        }
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

    public bool IsCarried()
    {
        return isCarried;
    }

    public BallPoint GetBallPoint()
    {
        return ballPoint;
    }
}
