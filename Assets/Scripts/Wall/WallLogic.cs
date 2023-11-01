using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLogic : MonoBehaviour
{
    [SerializeField] TeamSide side; 

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Attacker")
        {
            AttackerAI attacker = other.gameObject.GetComponent<AttackerAI>();

            if (side != attacker.GetSide())
            {
                attacker.Dead();
            }
        }
    }
}
