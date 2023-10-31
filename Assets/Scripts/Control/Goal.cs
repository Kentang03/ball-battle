using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] TeamSide side;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Goalll");
            // Raise Event (Add Point)

            // Raise Event (New Round)
        }
    }
}
