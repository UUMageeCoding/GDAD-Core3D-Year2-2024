using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    [Space(20)]
    ///  USE THIS TO DETERMINE WHICH EVENT TO SEND (Mutiple scripts can be attached to the same object)
    /// Loop through the AudioEventSender_BGM scripts on the object and send the event with the matching eventName
    public string eventName = "Custom BGM Event Name";

    
    [Space(10)]
    [Header("Background Music Event Parameters (BGM)")]
    [Space(20)]
    public int musicTrackNumber = 0; // WILL USE THE TRACK NUMBER IF NO NAME IS GIVEN
    public string musicTrackName = "TRACK NAME HERE"; //IF NO NAME IS GIVEN, THE TRACK NUMBER WILL BE USED
    
    [Space(20)]
    public bool playOnEnabled = true;
    public bool loopBGM = true;
    
    [Space(10)]
    [Range(0,1f)]
    public float volume = 0.8f;
    public FadeType fadeType = FadeType.FadeInOut;
    [Range(0,5f)]
    public float fadeDuration = 1.5f;
    
    [Space(10)]
    [Range(0,5f)]
    public float eventDelay = 0f;
    
    [Space(20)]
    [Header("TestMode : 'M' to play music, 'N' to stop, 'B' to pause")]
    public bool testMode = false;

    private void OnEnable(){
        if (playOnEnabled)
        {   
            //CHECK THE TIME THE GAME HAS BEEN RUNNING - The audiomanager will not be ready to play music until the start method has run
            if (Time.timeSinceLevelLoad > 0.1f){
                Play();
            }
            else{
                StartCoroutine(PlayBGM_Delayed(eventDelay)); 
            }
        }
    }

    private void OnDisable(){
        if (AudioManager.Instance.isActiveAndEnabled){
            Stop();
        }
    }

    public void Play()
    {
        if(eventDelay <= 0)
        {
            PlayBGM();
        }
        else
        {
            StartCoroutine(PlayBGM_Delayed(eventDelay));
        }
    }
    private void PlayBGM()
    {
        //send the PlayBGM Event with parameters from the inspector
        AudioEventManager.PlayBGM(musicTrackNumber, musicTrackName, volume, fadeType, fadeDuration, loopBGM);
    }
    
    private IEnumerator PlayBGM_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the PlayBGM Event with parameters from the inspector
        AudioEventManager.PlayBGM(musicTrackNumber, musicTrackName, volume, fadeType, fadeDuration, loopBGM);
       
    }

    public void Stop(){
        if(eventDelay <= 0){
            StopBGM();
        }
        else
        {
            StartCoroutine(StopBGM_Delayed(eventDelay));
        }
    }
    private void StopBGM()
    {
        //send the StopBGM Event with parameters from the inspector
        AudioEventManager.StopBGM(fadeDuration);
    }
    private IEnumerator StopBGM_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the StopBGM Event with parameters from the inspector
        AudioEventManager.StopBGM(fadeDuration);
    }
    
    // pause the background music
    public void Pause()
    {
        if(eventDelay <= 0)
        {
            PauseBGM();
        }
        else
        {
            StartCoroutine(PauseBGM_Delayed(eventDelay));
        }
    }
    private void PauseBGM()
    {
        //send the PauseBGM Event with parameters from the inspector
        AudioEventManager.PauseBGM(fadeDuration);
    }
    private IEnumerator PauseBGM_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the PauseBGM Event with parameters from the inspector
        AudioEventManager.PauseBGM(fadeDuration);
    }
    
    
    //----------------- EDITOR / TESTING-----------------
    // This section is only used in the editor to test the events - TODO ADD A CUSTOM EDITOR SCRIPT TO CALL THESE METHODS
    void Update()
    {
        if (testMode){
            if (Input.GetKeyDown(KeyCode.M))
            {
                Play();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                Stop();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Pause();
            }
        }

    }
}
