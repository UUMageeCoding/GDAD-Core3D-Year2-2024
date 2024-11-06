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
    public delegate void AudioEvent_PlayBGM(int index, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loopBGM, string eventName);
    public delegate void AudioEvent_StopBGM(float fadeDuration);
    public delegate void AudioEvent_PauseBGM(float fadeDuration);
    
    //define a delegate for audio events - Ambient Music
    public delegate void AudioEvent_PlayAmbientAudio(int index, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loopBGM, string eventName);
    public delegate void AudioEvent_StopAmbientAudio(float fadeDuration);
    public delegate void AudioEvent_PauseAmbientAudio(float fadeDuration);
    
    
    // Define a delegate for audio events - SFX
    public delegate void AudioEvent_PlaySFX(Transform attachTo, string soundName, float volume, float pitch, bool randomizePitch, float pitchRange,  float spatialBlend, string eventName);
    
    
    // --- Events --- BGM
    // playing background music
    public static AudioEvent_PlayBGM PlayBGM;
    // stopping background music
    public static AudioEvent_StopBGM StopBGM;
    // pausing background music
    public static AudioEvent_PauseBGM PauseBGM;
    
    
    // --- Events --- Ambient Music
    // playing ambient music
    public static AudioEvent_PlayAmbientAudio PlayAmbientMusic;
    // stopping ambient music
    public static AudioEvent_StopAmbientAudio StopAmbientMusic;
    // pausing ambient music
    public static AudioEvent_PauseAmbientAudio PauseAmbientMusic;
    
    
    // --- Events --- SFX
    // Multi-delegate for playing sound effects
    public static AudioEvent_PlaySFX PlaySFX;
    
    
}