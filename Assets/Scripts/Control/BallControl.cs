using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] bool isCarrying;

    void OnTriggerStay(Collider other) {
        Debug.Log("collide " + other.gameObject.tag);
        if (other.gameObject.tag == "Attacker")
        {
            this.transform.position = FindObjectOfType<BallPoint>().transform.position;
            isCarrying = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Attacker")
        {
            isCarrying = false;
        }
    }
}
