using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    public int musicTrackNumber = 0;
    
    [Space(20)]
    [Header("Sound FX Event Parameters")]
    [Space(5)]
    // parameters to pass with the SFX event
    public string sfxName = "name";
    [Range(0,1f)]
    public float volume = 1.0f;
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
            //AudioManager.Instance.PlayMusic(musicTrackNumber);
        }
        
        //play sound effect when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //example with explicit parameters passed
            //AudioEventManager.OnPlaySFX(this.transform, "SFX Name Here", 1.0f, 1.0f, true, 0.8f, 1.2f, 0f);
            
            //example with parameters from the inspector
            AudioEventManager.OnPlaySFX(this.transform, sfxName, volume, pitch, randomisePitch, pitchRange, spatialBlend);
        }
    }
}
