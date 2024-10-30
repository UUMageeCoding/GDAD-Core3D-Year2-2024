using System;
using UnityEngine;

//define enums for fade types
public enum FadeType
{
    FadeInOut,
    Crossfade
}

// A simple class to define delegates for audio-related events
public static class AudioEventManager
{
    
    //define a delegate for audio events - BGM
    public delegate void AudioEventBGM(int index, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loopBGM);
    
    // Define a delegate for audio events - SFX
    public delegate void AudioEventSFX(Transform attachTo, string soundName, float volume, float pitch, bool randomizePitch, float pitchRange,  float spatialBlend);
    
    // Multi-delegate for playing background music
    public static AudioEventBGM PlayBGM;
    
    // Multi-delegate for playing sound effects
    public static AudioEventSFX PlaySFX;
    
    
}