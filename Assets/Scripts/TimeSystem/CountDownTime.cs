using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDownTime : MonoBehaviour
{
    public string scenename;
    float currentTime = 0f;
    public float startingTime = 10f;

    public TMP_Text countdownText;

    void Start() 
    {
        currentTime = startingTime;    
    }

    void Update() 
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
        }

        if(currentTime == 0)
        {
            LoadScene(scenename);
        }    
    }

    public void LoadScene(string scenename)
    {

        SceneManager.LoadScene(scenename);
    }

}   

