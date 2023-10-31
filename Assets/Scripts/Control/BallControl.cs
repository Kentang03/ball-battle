using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] bool isCarrying;
    [SerializeField] float force;
    [SerializeField] BallPoint ballPoint;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        isCarrying = false; 
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isCarrying = false;
            rb.AddForce(transform.rotation * Vector3.forward * force, ForceMode.Impulse);
        }

        if (isCarrying)
        {
            this.transform.position = ballPoint.transform.position;
            this.transform.rotation = ballPoint.transform.rotation;
        }
    }



    void OnTriggerEnter(Collider other) {
        Debug.Log("collide " + other.gameObject.tag);
        if (other.gameObject.tag == "Attacker")
        {
            ballPoint = FindObjectOfType<BallPoint>();
            isCarrying = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Attacker")
        {
            ballPoint = null;
            isCarrying = false;
        }
    }
}
