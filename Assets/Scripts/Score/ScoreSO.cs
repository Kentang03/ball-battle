using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Score", menuName = "ScriptableObject/Score", order = 1)]
    public class ScoreSO : ScriptableObject
    {
        public int redScore;
        public int blueScore;

        private void OnValidate()
        {
            redScore = 0;
            blueScore = 0;
        }
        
        
    }