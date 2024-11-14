using System;
using UnityEngine;

//define enums for fade types
public enum FadeType
{
    FadeInOut,
    Crossfade
}
public enum CollisionType
{
    Null,
    Collision,
    Trigger
}

// A simple class to define delegates for audio-related events
public static class AudioEventManager
{
    
    //define a delegate for audio events - BGM
    public delegate void AudioEvent_PlayBGM(int index, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loopBGM, string eventName);
    public delegate void AudioEvent_StopBGM(float fadeDuration);
    public delegate void AudioEvent_PauseBGM(float fadeDuration);
    
    //define a delegate for audio events - Ambient Music
    public delegate void AudioEvent_PlayAmbientAudio(Transform attachTo, int index, string trackName, float volume, float pitch, float spatialBlend, FadeType fadeType, float fadeDuration, bool loopAmbient, string eventName);
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
    
    
    // --- Events --- Ambient Audio
    // playing ambient audio
    public static AudioEvent_PlayAmbientAudio PlayAmbientAudio;
    // stopping ambient audio
    public static AudioEvent_StopAmbientAudio StopAmbientAudio;
    // pausing ambient audio
    public static AudioEvent_PauseAmbientAudio PauseAmbientAudio;
    
    
    // --- Events --- SFX
    // Multi-delegate for playing sound effects
    public static AudioEvent_PlaySFX PlaySFX;
    
    
}