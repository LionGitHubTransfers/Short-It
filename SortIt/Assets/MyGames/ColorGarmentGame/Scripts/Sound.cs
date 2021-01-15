using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.0f,3.0f)]
    public float Volume, pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
