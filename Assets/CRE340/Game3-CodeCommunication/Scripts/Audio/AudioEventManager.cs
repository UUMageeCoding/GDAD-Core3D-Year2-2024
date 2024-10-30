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
    public delegate void AudioEvent_PlayBGM(int index, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loopBGM);
    public delegate void AudioEvent_StopBGM(float fadeDuration);
    public delegate void AudioEvent_PauseBGM(float fadeDuration);
    
    // Define a delegate for audio events - SFX
    public delegate void AudioEvent_PlaySFX(Transform attachTo, string soundName, float volume, float pitch, bool randomizePitch, float pitchRange,  float spatialBlend);
    
    // --- Multi-delegates ---
    
    // Multi-delegate for playing background music
    public static AudioEvent_PlayBGM PlayBGM;
    
    // Multi-delegate for stopping background music
    public static AudioEvent_StopBGM StopBGM;
    
    // Multi-delegate for pausing background music
    public static AudioEvent_PauseBGM PauseBGM;
    
    // Multi-delegate for playing sound effects
    public static AudioEvent_PlaySFX PlaySFX;
    
    
}