using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioTester : MonoBehaviour
{
    [Space(20)]
    [Header("Background Music Event Parameters (BGM)")]
    [Space(5)]
    public int musicTrackNumber = -1; 
    public string musicTrackName = "name";
    [Range(0,1f)]
    public float bgmVolume = 0.6f;
    public FadeType fadeType = FadeType.Crossfade;
    [Range(0,5f)]
    public float fadeDuration = 1.5f;
    public bool loopBGM = true;
    
    [Space(20)]
    [Header("Sound FX Event Parameters (SFX)")]
    [Space(5)]
    // parameters to pass with the SFX event
    public string sfxName = "name";
    [FormerlySerializedAs("volume")] [Range(0,1f)]
    public float sfxVolume = 1.0f;
    [Range(0,2f)]
    public float pitch = 1.0f;
    public bool randomisePitch = true;
    [Range(0,1f)]
    public float pitchRange = 0.2f;
    [Range(0,1f)]
    public float spatialBlend = 0.0f;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //playe background music  when a number key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            //example with explicit parameters passed
            //AudioEventManager.PlayBGM(0, "Music Name Here", 1.0f, FadeType.Crossfade, 2f,true);
            
            //example with parameters from the inspector
            AudioEventManager.PlayBGM(musicTrackNumber, musicTrackName, bgmVolume, fadeType, fadeDuration, loopBGM);
        }
        
        //play sound effect when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //example with explicit parameters passed
            //AudioEventManager.OnPlaySFX(this.transform, "SFX Name Here", 1.0f, 1.0f, true, 0.8f, 1.2f, 0f);
            
            //example with parameters from the inspector
            AudioEventManager.PlaySFX(this.transform, sfxName, sfxVolume, pitch, randomisePitch, pitchRange, spatialBlend);
        }
    }
}
