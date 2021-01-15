using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LionStudios;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public string levelName, levelScore,HighScore;
    public TMP_Text tmpLevelName, tmpScore,highScore;
    AudioManager audioManeger;
    public GameObject ManagerPanal,HomePanal;
    int currentScene;
    public bool complete;
    public TMP_Text NextorSkip;

    void Start()
    {
        audioManeger = GameObject.FindObjectOfType<AudioManager>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        levelName = (currentScene + 1).ToString();
        Analytics.Events.LevelStarted(levelName, levelScore);
        complete = false;
        HomePanal.SetActive(false);
    }

   
    void Update()
    {
        if(ManagerPanal.activeSelf == true)
        {
            tmpLevelName.text = "Level "+levelName;
            tmpScore.text = levelScore;
            highScore.text = "Highest Score : "+HighScore;
        }
        if (complete == true)
        {
            NextorSkip.text = "NEXT";
            
        }
        else
        {
            NextorSkip.text = "SKIP";

        }

    }
    public void Replay() 
    {
        SceneManager.LoadScene(currentScene);
    }
    public void Next() 
    {
        sendEvents();

        if (currentScene == 3)
        {
            // Analytics.Events.AllLevelsComplete();
            SceneManager.LoadScene(0);

        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);

        }
    }
    public void Home()
    {
        sendEvents();
        HomePanal.SetActive(true);
        
    }
    public void Exit() 
    {
        sendEvents();
        Application.Quit();
    }
    public void LaunchPanal() 
    {
        //if (ManagerPanal.activeSelf==false)
        //{
            ManagerPanal.SetActive(true);
        //}
    }
    void sendEvents() 
    {
        if (complete == true)
        {
        Analytics.Events.LevelComplete(levelName, levelScore);

        }
        else
        {
            Analytics.Events.LevelSkipped(levelName, levelScore);

        }

    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
