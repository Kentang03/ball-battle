using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLogic : MonoBehaviour
{
    [SerializeField] TeamSide side; 

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Attacker")
        {
            if (side != other.gameObject.GetComponent<AttackerAI>().GetSide())
            {
                Debug.Log(other.name + "Collide with wall");
            }
        }
    }
}
