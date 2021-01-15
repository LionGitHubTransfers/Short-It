using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LionStudios;

public class ProgressBar : MonoBehaviour
{
    Slider ProgressSlider;
    public int maxValue;
    public int Value;
    public GameObject Confetti;
    float Timer;
    float Score;
    int currentScene;
    int wrongBox;
    string levelname,Levelscore;
    AudioManager audioManager;
    bool levelstarted;
    GameManagerScript gameManeger;

    
    void Start()
    {
        gameManeger = GameObject.FindObjectOfType<GameManagerScript>();
        Confetti = Camera.main.transform.GetChild(2).gameObject;
        levelstarted = true;
       // currentScene = SceneManager.GetActiveScene().buildIndex;
        audioManager = FindObjectOfType<AudioManager>();
        ProgressSlider = GetComponent<Slider>();
        ProgressSlider.maxValue = maxValue;
        ProgressSlider.value = Value;
        Timer = 0;
        Score = 0;
        wrongBox = 0;

        //levelname = (currentScene + 1).ToString();

        //Analytics.Events.LevelStarted(currentScene + 1);
        // Analytics.Events.LevelStarted(levelname,Levelscore);

        //print("Started Level : " + (currentScene + 1) + "YourHighScore : " + PlayerPrefs.GetInt("HighScore", 0));
        int highsc = PlayerPrefs.GetInt("HighScore", 0);
        gameManeger.HighScore = highsc.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
       
        ProgressSlider.maxValue = maxValue;

        if(ProgressSlider.value < Value)
        {

            ProgressSlider.value += Time.deltaTime*1.5f;

        }

        if (Value == maxValue)
        {

            StartCoroutine(WaitSec(1f));

        }

    }
    IEnumerator WaitSec(float sec)
    {
        if(levelstarted==true && Value == maxValue)
        {
            levelstarted = false;
            gameManeger.complete = true;

            audioManager.PlaySound("Cheer");

            Confetti.SetActive(true);

            Score = ((maxValue * 100) / Timer);
            int RoundScore = Mathf.RoundToInt(Score);

            // Analytics.LogEvent("Level Completeing Time ", levelname, Timer);

            //string currentLevel = (currentScene + 1).ToString();
           //levelname = (currentScene + 1).ToString();
           // string score = RoundScore.ToString();
            Levelscore = RoundScore.ToString();
            gameManeger.levelScore = Levelscore;
            //gameManeger.LaunchPanal();
           // Analytics.Events.LevelComplete(levelname, Levelscore);

            if (RoundScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", RoundScore);
                //Analytics.Events.HighScoreUnlocked(currentScene + 1, RoundScore);
                Levelscore = RoundScore.ToString();
                
                Analytics.Events.HighScoreUnlocked(levelname, Levelscore);

               // print("Complete Level : " + (currentScene + 1) + "HighScore : " + RoundScore);



            }


            //Analytics.Events.LevelComplete(currentScene + 1, RoundScore);

            //print("ufciuhcvs");

            yield return new WaitForSeconds(sec);
            gameManeger.LaunchPanal();
            if (gameManeger.ManagerPanal.activeSelf == false)
            {
                gameManeger.LaunchPanal();
            }
            //if (currentScene == 3)
            //{
            //   // Analytics.Events.AllLevelsComplete();
            //    SceneManager.LoadScene(0);

            //}
            //else
            //{
            //    SceneManager.LoadScene(currentScene + 1);

            //}


        }



    }
    public void WrongBox()
    {
        //Analytics.Events.AllLevelsComplete();
        //
        wrongBox++;
        Analytics.LogEvent("WrongBox/ faild Attempts", levelname , wrongBox.ToString());
    }

}
