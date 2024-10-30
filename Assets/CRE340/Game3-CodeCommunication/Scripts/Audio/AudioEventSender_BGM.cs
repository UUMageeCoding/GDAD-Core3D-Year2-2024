using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// to be attached to a GameObject that will send an audio event TO PLAY BACKGROUND MUSIC
/// USAGE:
///     attach this script to a GameObject and call the PlayBGM method from an event trigger or script
///     to play music with the parameters set in the inspector
///     ...
///     
/// </summary>

public class AudioEventSender_BGM : MonoBehaviour
{
    [Space(10)]
    [Header("Background Music Event Parameters (BGM)")]
    [Space(20)]
    public int musicTrackNumber = 0; // WILL USE THE TRACK NUMBER IF NO NAME IS GIVEN
    public string musicTrackName = "TRACK NAME HERE"; //IF NO NAME IS GIVEN, THE TRACK NUMBER WILL BE USED
    
    [Space(20)]
    public bool playOnAwake = true;
    public bool loopBGM = true;
    
    [Space(10)]
    [Range(0,1f)]
    public float bgmVolume = 0.8f;
    public FadeType fadeType = FadeType.FadeInOut;
    [Range(0,5f)]
    public float fadeDuration = 1.5f;
    [Range(0,5f)]
    public float playDelay = 0f;

    private void OnEnable(){
        if (playOnAwake)
        {   
            Play();
        }
    }

    public void Play()
    {
        if(playDelay == 0)
        {
            //send the PlayBGM Event with parameters from the inspector
            AudioEventManager.PlayBGM(musicTrackNumber, musicTrackName, bgmVolume, fadeType, fadeDuration, loopBGM);
        }
        else
        {
            StartCoroutine(PlayDelayed(playDelay));
        }
    }
    public void Play(float delay)
    {
        StartCoroutine(PlayDelayed(delay));
    }
    
    public IEnumerator PlayDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the PlayBGM Event with parameters from the inspector
        AudioEventManager.PlayBGM(musicTrackNumber, musicTrackName, bgmVolume, fadeType, fadeDuration, loopBGM);
       
    }
    
}
