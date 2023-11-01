using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject loseUI;
    public GameObject winUI;
    bool active;

    private void Update() {
        if (loseUI == null || loseUI == null)
        {
            return;
        }
    }

    public void SetWinUI()
    {
        winUI.SetActive(true);
        loseUI.SetActive(false);
        Time.timeScale = 0;
       
    }

    public void SetLoseUI()
    {
        winUI.SetActive(false);
        loseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenAndClose()
    {
        if(active == false)
        {
            winUI.SetActive(true);
            active = true;
            Time.timeScale = 0;
        }
        else
        {
            winUI.SetActive(false);
            active = false;
            Time.timeScale = 1;
        }
    }

    public void LoadScene(string scenename)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scenename);
    }
}
