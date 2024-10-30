using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// to be attached to a GameObject that will send an audio event TO PLAY A SOUND EFFECT
/// USAGE:
///     attach this script to a GameObject and call the PlaySFX method from an event trigger or script
///     to play a sound effect with the parameters set in the inspector
///     ...
///     or call the PlaySFX method with a Transform parameter to attach the sound to a different GameObject
/// </summary>

public class AudioEventSender_SFX : MonoBehaviour
{
    
    [Space(20)]
    ///  USE THIS TO DETERMINE WHICH EVENT TO SEND (Mutiple scripts can be attached to the same object)
    /// Loop through the AudioEventSender_SFX scripts on the object and send the event with the matching eventName
    public string eventName = "Custom BGM Event Name"; 
    
    [Space(10)] 
    [Header("Sound FX Event Parameters (SFX)")] [Space(5)]
    [Space(20)]
    public string sfxName = "SFM NAME HERE";
    
    [Space(20)]
    public bool playOnEnabled = true;
    public bool attachSoundToTransform = false;
    
    [Space(10)]
    [Range(0, 1f)] public float volume = 1.0f;
    [Range(0, 2f)] public float pitch = 1.0f;
    public bool randomisePitch = true;
    [Range(0, 1f)] public float pitchRange = 0.1f;
    [Range(0, 1f)] public float spatialBlend = 0.5f;

    [Space(10)]
    [Range(0,5f)]
    public float eventDelay = 0f;


    private void OnEnable(){
        if (playOnEnabled)
        {   
            //CHECK THE TIME THE GAME HAS BEEN RUNNING - The audiomanager will not be ready to play music until the start method has run
            if (Time.timeSinceLevelLoad > 0.1f){
                Play();
            }
            else{
                StartCoroutine(PlaySFX_Delayed(eventDelay)); 
            }
        }
    }
    
    public void Play(){
        if(eventDelay <= 0)
        {
            PlaySFX();
        }
        else
        {
            StartCoroutine(PlaySFX_Delayed(eventDelay));
        }
    }
    
    private void PlaySFX()
    {
        if (attachSoundToTransform){
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(this.transform, sfxName, volume, pitch, randomisePitch, pitchRange, spatialBlend);
        }
        else{
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(null, sfxName, volume, pitch, randomisePitch, pitchRange, spatialBlend);
        }
    }
    private IEnumerator PlaySFX_Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (attachSoundToTransform){
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(this.transform, sfxName, volume, pitch, randomisePitch, pitchRange, spatialBlend);
        }
        else{
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(null, sfxName, volume, pitch, randomisePitch, pitchRange, spatialBlend);
        }
    }
}