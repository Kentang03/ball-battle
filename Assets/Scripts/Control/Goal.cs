using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    [SerializeField] public TeamSide side;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball")
        {
            if (this.side == TeamSide.Red)
            {
                scoreSystem.BlueScore();
                
            }

            else if (this.side == TeamSide.Blue)
            {
                scoreSystem.RedScore();
            }

            // Raise Event (Add Point)

            // Raise Event (New Round)
        }
    }

    

     
}
