using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// to be attached to a GameObject that will send an audio event TO PLAY A SOUND EFFECT
/// USAGE:
///     attach this script to a GameObject and call the PlaySFX method from an event trigger or script
///     to play a sound effect with the parameters set in the inspector
///     ...
///     or call the PlaySFX method with a Transform parameter to attach the sound to a different GameObject
/// </summary>

public class AudioEventSender_SFX : MonoBehaviour{
    
    [Space(20)] 
    [Header("Sound FX Event Parameters (SFX)")] [Space(5)]
    // parameters to pass with the SFX event
    public string sfxName = "SFM NAME HERE";
    [Range(0, 1f)] public float sfxVolume = 1.0f;
    [Range(0, 2f)] public float pitch = 1.0f;
    public bool randomisePitch = true;
    [Range(0, 1f)] public float pitchRange = 0.1f;
    [Range(0, 1f)] public float spatialBlend = 0.5f;


    public void PlaySFX()
    {
        //send the PlaySFX Event with parameters from the inspector
        AudioEventManager.PlaySFX(this.transform, sfxName, sfxVolume, pitch, randomisePitch, pitchRange, spatialBlend);
    }
    public void PlaySFX(Transform attachTo)
    {
        //send the PlaySFX Event with parameters from the inspector and an additional Transform parameter to attach the sound to
        AudioEventManager.PlaySFX(attachTo, sfxName, sfxVolume, pitch, randomisePitch, pitchRange, spatialBlend);
    }
}