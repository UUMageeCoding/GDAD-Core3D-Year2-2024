using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventSender_Ambient : MonoBehaviour, IAudioEventSender
{
    [Space(20)]
    ///  USE THIS TO DETERMINE WHICH EVENT TO SEND (Multiple scripts can be attached to the same object)
    /// Loop through the AudioEventSender_Ambient scripts on the object and send the event with the matching eventName
    public string eventName = "Custom Ambient Event Name"; //for future use

    [Space(20)]
    [Header("Transform that the ambient audio will attach to - leave empty to attach to AudioManager")]
    public Transform attachTo;
    [Space(10)]
    [Header("Ambient Audio Event Parameters")]
    [Space(20)]
    [Tooltip("The track number of the ambient audio to play - used if no name is given -1 to ignore")]
    public int ambientTrackNumber = 0; // WILL USE THE TRACK NUMBER IF NO NAME IS GIVEN
    public string ambientTrackName = "TRACK NAME HERE"; //IF NO NAME IS GIVEN, THE TRACK NUMBER WILL BE USED

    [Space(20)]
    public bool playOnEnabled = true;
    public bool loopAmbient = true;

    [Space(10)]
    [Range(0, 1f)]
    public float volume = 0.8f;
    [Range(0, 2f)] public float pitch = 1.0f;
    [Range(0, 1f)] public float spatialBlend = 0.5f;
    public FadeType fadeType = FadeType.FadeInOut;
    [Range(0, 10f)]
    public float fadeDuration = 1.5f;

    [Space(10)]
    [Range(0, 5f)]
    public float eventDelay = 0f;

    [Space(20)]
    [Header("TestMode : 'M' to play ambient, 'N' to stop, 'B' to pause")]
    public bool testMode = false;

    

    private void OnEnable()
    {
        if (playOnEnabled)
        {
            //CHECK THE TIME THE GAME HAS BEEN RUNNING - The audiomanager will not be ready to play ambient until the start method has run
            if (Time.timeSinceLevelLoad > 0.1f)
            {
                Play();
            }
            else
            {
                StartCoroutine(PlayAmbient_Delayed(eventDelay));
            }
        }
    }

    private void OnDisable()
    {
        if (AudioManager.Instance.isActiveAndEnabled)
        {
            Stop();
        }
    }

    public void Play()
    {
        if (eventDelay <= 0)
        {
            PlayAmbient();
        }
        else
        {
            StartCoroutine(PlayAmbient_Delayed(eventDelay));
        }
    }

    private void PlayAmbient()
    {
        //send the PlayAmbient Event with parameters from the inspector
        AudioEventManager.PlayAmbientAudio(attachTo,ambientTrackNumber, ambientTrackName, volume, pitch, spatialBlend, fadeType, fadeDuration, loopAmbient, eventName);
    }

    private IEnumerator PlayAmbient_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the PlayAmbient Event with parameters from the inspector
        AudioEventManager.PlayAmbientAudio(attachTo, ambientTrackNumber, ambientTrackName, volume, pitch, spatialBlend, fadeType, fadeDuration, loopAmbient, eventName);
    }

    public void Stop()
    {
        if (eventDelay <= 0)
        {
            StopAmbient();
        }
        else
        {
            StartCoroutine(StopAmbient_Delayed(eventDelay));
        }
    }

    private void StopAmbient()
    {
        //send the StopAmbient Event with parameters from the inspector
        AudioEventManager.StopAmbientAudio(fadeDuration);
    }

    private IEnumerator StopAmbient_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the StopAmbient Event with parameters from the inspector
        if (AudioManager.Instance.isActiveAndEnabled){
            AudioEventManager.StopAmbientAudio(fadeDuration);
        }
    }

    // pause the ambient audio
    public void Pause()
    {
        if (eventDelay <= 0)
        {
            PauseAmbient();
        }
        else
        {
            StartCoroutine(PauseAmbient_Delayed(eventDelay));
        }
    }

    private void PauseAmbient()
    {
        //send the PauseAmbient Event with parameters from the inspector
        AudioEventManager.PauseAmbientAudio(fadeDuration);
    }

    private IEnumerator PauseAmbient_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //send the PauseAmbient Event with parameters from the inspector
        AudioEventManager.PauseAmbientAudio(fadeDuration);
    }

    //----------------- EDITOR / TESTING-----------------
    // This section is only used in the editor to test the events - TODO ADD A CUSTOM EDITOR SCRIPT TO CALL THESE METHODS
    void Update()
    {
        if (testMode)
        {
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