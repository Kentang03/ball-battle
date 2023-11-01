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

        if (this.transform.position.y < 0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 5f, this.transform.position.z);
        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     isCarried = false;
        //     rb.AddForce(transform.rotation * Vector3.forward * force, ForceMode.Impulse);
        // }

        if (isCarried)
        {
            this.transform.position = ballPoint.transform.position;
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
