using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;
    public string scenename;
    // private string blueScore;
    // private string redScore;
    public ScoreSO scoreSO;

    private void Update() 
    {
        blueScoreText.text = "score : " + scoreSO.blueScore;
        redScoreText.text = "score : " + scoreSO.redScore;
    }
    
    public void BlueScore()
    {
        scoreSO.blueScore += 1;
        // blueScore += 1;
        blueScoreText.text = "score : " + scoreSO.blueScore;
        // LoadScene(scenename);
    }

  

    public void RedScore()
    {
        scoreSO.redScore += 1;
        // redScore += 1;
        redScoreText.text = "score : " + scoreSO.redScore;
        // LoadScene(scenename);
    }



    public void LoadScene(string scenename)
    {

        SceneManager.LoadScene(scenename);
    }

}
