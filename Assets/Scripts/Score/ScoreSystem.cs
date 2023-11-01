using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;
    public MenuManager menuManager;
    public string scenename;
    public int winScore = 5;
    // private string blueScore;
    // private string redScore;
    public ScoreSO scoreSO;

    private void Update() 
    {
        blueScoreText.text = "score : " + scoreSO.blueScore;
        redScoreText.text = "score : " + scoreSO.redScore;
        EndCondition();
    }
    
    public void BlueScore()
    {
        scoreSO.blueScore += 1;
        // blueScore += 1;
        blueScoreText.text = "score : " + scoreSO.blueScore;
        LoadScene(scenename);
    }

  

    public void RedScore()
    {
        scoreSO.redScore += 1;
        // redScore += 1;
        redScoreText.text = "score : " + scoreSO.redScore;
        LoadScene(scenename);
    }

    public void EndCondition()
    {
        if(scoreSO.blueScore == winScore)
        {
            menuManager.SetWinUI();
        }
        else if (scoreSO.redScore == winScore)
        {
            menuManager.SetLoseUI();
        }
    }



    public void LoadScene(string scenename)
    {

        SceneManager.LoadScene(scenename);
    }

}
