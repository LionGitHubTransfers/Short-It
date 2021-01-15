using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool Music;
    public bool SFX;
    public Sound[] sounds;
    public static AudioManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

           
        }
    }

    // Update is called once per frame
    private void Start()
    {
        if (Music)
        {
            PlaySound("Theme");

        }
        


        //print("Theme Sfx");
    }
    public void PlaySound(string name)
    {
        if (SFX)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                return;
            }
            s.source.Play();
        }
        
        //print("Other Sfx");

    }
}
