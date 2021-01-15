using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LionStudios;
using UnityEngine.UI;

public class MyLionTest : MonoBehaviour
{
    public object level,score;
    public InputField LevelTnput,ScoreInput;
    void Start()
    {
        
    }

    
    void Update()
    {
        level = LevelTnput.text;
        score = ScoreInput.text;
    }
    public void LevelStart()
    {
        Analytics.Events.LevelStarted(level,score);
    }
    public void LevelComplete()
    {
        Analytics.Events.LevelComplete(level,score);
    }
    public void LevelFailed()
    {
        Analytics.Events.LevelFailed(level,score);
    }


}
