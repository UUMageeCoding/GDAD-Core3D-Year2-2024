using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// The AudioManager class is responsible for managing background music and sound effects in the game.
/// It handles loading audio resources, playing, stopping, and pausing background music with fade effects,
/// and playing sound effects with various parameters such as volume, pitch, and spatial blend.
/// This class uses the singleton pattern to ensure only one instance is active at any time.
/// The methods of this class are called via events defined in the AudioEventManager.
/// </summary>

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music Settings")]
    public GameObject musicPrefab;
    private float musicFadeDuration = 1.5f;
    private FadeType musicFadeType = FadeType.Crossfade;
    public bool isFadingMusic = false; // Flag to prevent multiple fades at once
    private bool isPausedMusic = false; // Tracks if the music is paused

    private Dictionary<int, AudioClip> musicTracks = new Dictionary<int, AudioClip>();
    private AudioSource currentMusicSource;
    private AudioSource nextMusicSource;

    
    [Header("Ambient Audio Settings")]
    public GameObject ambientAudioPrefab;
    private float ambientFadeDuration = 1.5f;
    private FadeType ambientFadeType = FadeType.Crossfade;
    public bool isFadingAmbientAudio = false; // Flag to prevent multiple fades at once
    private bool isPausedAmbientAudio = false; // Tracks if the ambient audio is paused
    
    private Dictionary<int, AudioClip> ambientAudioTracks = new Dictionary<int, AudioClip>();
    private AudioSource currentAmbientAudioSource;
    private AudioSource nextAmbientAudioSource;
    
    
    [Header("Sound Effects Settings")]
    public GameObject soundEffectPrefab;
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    // --------------------------------------------------------------------------------------------
    [Header("Available Music Tracks")]
    [SerializeField] private List<string> musicTrackNames = new List<string>();
    
    [Header("Available Ambient Audio Tracks")]
    [SerializeField] private List<string> ambientAudioTrackNames = new List<string>();

    [Header("Available Sound Effects")]
    [SerializeField] private List<string> soundEffectNames = new List<string>();

    // --------------------------------------------------------------------------------------------
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioResources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --------------------------------------------------------------------------------------------
    #region Event Subscriptions ------------------------------------
    private void OnEnable()
    {
        AudioEventManager.PlayBGM += PlayMusic;
        AudioEventManager.StopBGM += StopMusic;
        AudioEventManager.PauseBGM += PauseMusic;
        
        AudioEventManager.PlayAmbientAudio += PlayAmbientAudio;
        AudioEventManager.StopAmbientAudio += StopAmbientAudio;
        AudioEventManager.PauseAmbientAudio += PauseAmbientAudio;
        
        AudioEventManager.PlaySFX += PlaySoundEffect;
    }

    private void OnDisable()
    {
        AudioEventManager.PlayBGM -= PlayMusic;
        AudioEventManager.StopBGM -= StopMusic;
        AudioEventManager.PauseBGM -= PauseMusic;
        
        AudioEventManager.PlayAmbientAudio -= PlayAmbientAudio;
        AudioEventManager.StopAmbientAudio -= StopAmbientAudio;
        AudioEventManager.PauseAmbientAudio -= PauseAmbientAudio;
        
        AudioEventManager.PlaySFX -= PlaySoundEffect;
    }
    #endregion 
    // --------------------------------------------------------------------------------------------
    
    // --------------------------------------------------------------------------------------------
    #region Load Audio Resources ------------------------------------
    private void LoadAudioResources()
    {
        AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Audio/BGM");
        for (int i = 0; i < bgmClips.Length; i++)
        {
            musicTracks[i] = bgmClips[i];
            musicTrackNames.Add(bgmClips[i].name);
        }
        
        AudioClip[] ambientClips = Resources.LoadAll<AudioClip>("Audio/Ambient");
        for (int i = 0; i < ambientClips.Length; i++)
        {
            ambientAudioTracks[i] = ambientClips[i];
            ambientAudioTrackNames.Add(ambientClips[i].name);
        }

        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Audio/SFX");
        foreach (var clip in sfxClips)
        {
            soundEffects[clip.name] = clip;
            soundEffectNames.Add(clip.name);
        }
    }
    #endregion
    // --------------------------------------------------------------------------------------------


    // --------------------------------------------------------------------------------------------
    #region Play Background Music ------------------------------------
    
    // Event Method - Play background music by track number or name with optional volume and loop settings - calls appropriate overload based on parameters
    public void PlayMusic(int trackNumber, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loop, string eventName)
    {
        if (isFadingMusic) return; // Block if a fade/crossfade is already in progress

        musicFadeType = fadeType;
        musicFadeDuration = fadeDuration;
        
        if (string.IsNullOrEmpty(trackName) && trackNumber >= 0)
        {
            PlayMusic(trackNumber, volume, loop);
        }
        else if (!string.IsNullOrEmpty(trackName))
        {
            PlayMusic(trackName, volume, loop);
        }
    }
    // Overload - Play background music by track number with optional volume and loop settings
    public void PlayMusic(int trackNumber, float volume, bool loop = true)
    {
        if (isFadingMusic) return; // Block if a fade/crossfade is already in progress

        if (!musicTracks.TryGetValue(trackNumber, out AudioClip newTrack)) return;
        isFadingMusic = true;
        if (musicFadeType == FadeType.Crossfade)
        {
            StartCoroutine(CrossfadeMusic(newTrack, volume, loop));
        }
        else
        {
            StartCoroutine(FadeOutAndInMusic(newTrack, volume, loop));
        }
    }

    public void PlayMusic(string trackName, float volume, bool loop = true)
    {
        if (isFadingMusic) return; // Block if a fade/crossfade is already in progress

        foreach (var track in musicTracks)
        {
            if (track.Value.name == trackName)
            {
                isFadingMusic = true;
                if (musicFadeType == FadeType.Crossfade)
                {
                    StartCoroutine(CrossfadeMusic(track.Value, volume, loop));
                }
                else
                {
                    StartCoroutine(FadeOutAndInMusic(track.Value, volume, loop));
                }
                return;
            }
        }
        Debug.LogWarning($"Music track '{trackName}' not found in Resources/Audio/BGM!");
    }

    private IEnumerator CrossfadeMusic(AudioClip newTrack, float targetVolume, bool loop)
    {
        float crossfadeDuration = musicFadeDuration;

        GameObject musicObject = Instantiate(musicPrefab, transform);
        nextMusicSource = musicObject.GetComponent<AudioSource>();
        nextMusicSource.clip = newTrack;
        nextMusicSource.volume = 0;  // Start volume at 0 for crossfade
        nextMusicSource.loop = loop;
        nextMusicSource.Play();

        if (currentMusicSource != null && currentMusicSource.isPlaying)
        {
            float startVolume = currentMusicSource.volume;
            for (float t = 0; t < crossfadeDuration; t += Time.deltaTime)
            {
                currentMusicSource.volume = Mathf.Lerp(startVolume, 0, t / crossfadeDuration);
                nextMusicSource.volume = Mathf.Lerp(0, targetVolume, t / crossfadeDuration);
                yield return null;
            }
            Destroy(currentMusicSource.gameObject); // Clean up old AudioSource after crossfade
        }

        nextMusicSource.volume = targetVolume;
        currentMusicSource = nextMusicSource;
        isFadingMusic = false; // Reset flag after crossfade completes
    }

    private IEnumerator FadeOutAndInMusic(AudioClip newTrack, float targetVolume, bool loop)
    {
        if (currentMusicSource != null && currentMusicSource.isPlaying)
        {
            float startVolume = currentMusicSource.volume;
            for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
            {
                currentMusicSource.volume = Mathf.Lerp(startVolume, 0, t / musicFadeDuration);
                yield return null;
            }
            currentMusicSource.Stop();
            Destroy(currentMusicSource.gameObject); // Clean up old AudioSource after fade out
        }

        GameObject musicObject = Instantiate(musicPrefab, transform);
        nextMusicSource = musicObject.GetComponent<AudioSource>();
        nextMusicSource.clip = newTrack;
        nextMusicSource.volume = 0;
        nextMusicSource.loop = loop;
        nextMusicSource.Play();

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            nextMusicSource.volume = Mathf.Lerp(0, targetVolume, t / musicFadeDuration);
            yield return null;
        }

        nextMusicSource.volume = targetVolume;
        currentMusicSource = nextMusicSource;
        isFadingMusic = false; // Reset flag after fade completes
    }
    #endregion
    // --------------------------------------------------------------------------------------------
    
    // --------------------------------------------------------------------------------------------
    #region StopBackgroundMusic ------------------------------------
    public void StopMusic(float fadeDuration)
    {
        musicFadeDuration = fadeDuration;
        
        // Check if there's music playing and that it's not already fading
        if (currentMusicSource != null && currentMusicSource.isPlaying && !isFadingMusic)
        {
            StartCoroutine(FadeOutCurrentMusic());
        }
    }

    private IEnumerator FadeOutCurrentMusic()
    {
        isFadingMusic = true;
        float startVolume = currentMusicSource.volume;

        // Fade out over musicFadeDuration
        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            currentMusicSource.volume = Mathf.Lerp(startVolume, 0, t / musicFadeDuration);
            yield return null;
        }

        // Stop and clean up the music source after fade-out
        currentMusicSource.Stop();
        Destroy(currentMusicSource.gameObject);
        currentMusicSource = null;  // Reset the currentMusicSource reference
        isFadingMusic = false; // Allow other fades to proceed
    }
    #endregion
    // --------------------------------------------------------------------------------------------

    // --------------------------------------------------------------------------------------------
    #region PauseBackgroundMusic ------------------------------------
    public void PauseMusic(float fadeDuration)
    {
        // Check if a fade is already in progress to avoid interruptions
        if (isFadingMusic) return;

        musicFadeDuration = fadeDuration; // Set the fade duration for pausing
        
        // Toggle pause state
        if (isPausedMusic)
        {
            // Resume the music with fade-in if currently paused
            StartCoroutine(FadeInMusic());
        }
        else
        {
            // Fade out and pause if currently playing
            StartCoroutine(FadeOutAndPauseMusic());
        }

        isPausedMusic = !isPausedMusic; // Toggle the pause state
    }
    private IEnumerator FadeOutAndPauseMusic()
    {
        isFadingMusic = true;
        float startVolume = currentMusicSource.volume;

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            currentMusicSource.volume = Mathf.Lerp(startVolume, 0, t / musicFadeDuration);
            yield return null;
        }

        currentMusicSource.Pause(); // Pause the music once fade-out completes
        isFadingMusic = false;
    }
    private IEnumerator FadeInMusic()
    {
        isFadingMusic = true;
        currentMusicSource.UnPause(); // Resume the music before fade-in
        float targetVolume = 1.0f; // Set to the desired full volume

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            currentMusicSource.volume = Mathf.Lerp(0, targetVolume, t / musicFadeDuration);
            yield return null;
        }

        currentMusicSource.volume = targetVolume; // Ensure final volume is set
        isFadingMusic = false;
    }

    #endregion
    // --------------------------------------------------------------------------------------------
    
    
    // --------------------------------------------------------------------------------------------
    #region PlayAmbientAudio ------------------------------------
    
    // Event Method - Play ambient by track number or name with optional volume and loop settings - calls appropriate overload based on parameters
    public void PlayAmbientAudio(Transform attachTo, int trackNumber, string trackName, float volume, float pitch, float spatialBlend, FadeType fadeType, float fadeDuration, bool loop, string eventName)
    {
        if (isFadingAmbientAudio) return; // Block if a fade/crossfade is already in progress

        ambientFadeType = fadeType;
        ambientFadeDuration = fadeDuration;

        if (string.IsNullOrEmpty(trackName) && trackNumber >= 0)
        {
            PlayAmbientAudio(attachTo, trackNumber, volume, pitch, spatialBlend, loop);
        }
        else if (!string.IsNullOrEmpty(trackName))
        {
            PlayAmbientAudio(attachTo, trackName, volume, pitch, spatialBlend, loop);
        }
    }

    public void PlayAmbientAudio(Transform attachTo, int trackNumber, float volume, float pitch, float spatialBlend, bool loop = true)
    {
        if (isFadingAmbientAudio) return; // Block if a fade/crossfade is already in progress

        if (!ambientAudioTracks.TryGetValue(trackNumber, out AudioClip newTrack)) return;
        isFadingAmbientAudio = true;
        if (ambientFadeType == FadeType.Crossfade)
        {
            StartCoroutine(CrossfadeAmbientAudio(attachTo, newTrack, volume, pitch, spatialBlend, loop));
        }
        else
        {
            StartCoroutine(FadeOutAndInAmbientAudio(attachTo, newTrack, volume, pitch, spatialBlend, loop));
        }
    }

    public void PlayAmbientAudio(Transform attachTo, string trackName, float volume, float pitch, float spatialBlend, bool loop = true)
    {
        if (isFadingAmbientAudio) return; // Block if a fade/crossfade is already in progress

        foreach (var track in ambientAudioTracks)
        {
            if (track.Value.name == trackName)
            {
                isFadingAmbientAudio = true;
                if (ambientFadeType == FadeType.Crossfade)
                {
                    StartCoroutine(CrossfadeAmbientAudio(attachTo, track.Value, volume, pitch, spatialBlend, loop));
                }
                else
                {
                    StartCoroutine(FadeOutAndInAmbientAudio(attachTo, track.Value, volume, pitch, spatialBlend, loop));
                }
                return;
            }
        }
        Debug.LogWarning($"Ambient audio track '{trackName}' not found in Resources/Audio/Ambient!");
    }

private IEnumerator CrossfadeAmbientAudio(Transform attachTo, AudioClip newTrack, float targetVolume, float targetPitch, float targetSpatialBlend, bool loop)
{
    float crossfadeDuration = ambientFadeDuration;

    if (attachTo == null)
    {
        attachTo = transform; // Default to AudioManager's transform if attachTo is null
    }

    GameObject ambientObject = Instantiate(ambientAudioPrefab, attachTo.position, Quaternion.identity, attachTo);
    nextAmbientAudioSource = ambientObject.GetComponent<AudioSource>();
    nextAmbientAudioSource.clip = newTrack;
    nextAmbientAudioSource.volume = 0;  // Start volume at 0 for crossfade
    nextAmbientAudioSource.pitch = targetPitch;
    nextAmbientAudioSource.spatialBlend = targetSpatialBlend;
    nextAmbientAudioSource.loop = loop;
    nextAmbientAudioSource.Play();

    if (currentAmbientAudioSource != null && currentAmbientAudioSource.isPlaying)
    {
        float startVolume = currentAmbientAudioSource.volume;
        float startPitch = currentAmbientAudioSource.pitch;
        float startSpatialBlend = currentAmbientAudioSource.spatialBlend;
        for (float t = 0; t < crossfadeDuration; t += Time.deltaTime)
        {
            currentAmbientAudioSource.volume = Mathf.Lerp(startVolume, 0, t / crossfadeDuration);
            nextAmbientAudioSource.volume = Mathf.Lerp(0, targetVolume, t / crossfadeDuration);
            nextAmbientAudioSource.pitch = Mathf.Lerp(startPitch, targetPitch, t / crossfadeDuration);
            nextAmbientAudioSource.spatialBlend = Mathf.Lerp(startSpatialBlend, targetSpatialBlend, t / crossfadeDuration);
            yield return null;
        }
        Destroy(currentAmbientAudioSource.gameObject); // Clean up old AudioSource after crossfade
    }

    nextAmbientAudioSource.volume = targetVolume;
    currentAmbientAudioSource = nextAmbientAudioSource;
    isFadingAmbientAudio = false; // Reset flag after crossfade completes
}

private IEnumerator FadeOutAndInAmbientAudio(Transform attachTo, AudioClip newTrack, float targetVolume, float targetPitch, float targetSpatialBlend, bool loop)
{
    if (attachTo == null)
    {
        attachTo = transform; // Default to AudioManager's transform if attachTo is null
    }

    if (currentAmbientAudioSource != null && currentAmbientAudioSource.isPlaying)
    {
        float startVolume = currentAmbientAudioSource.volume;
        for (float t = 0; t < ambientFadeDuration; t += Time.deltaTime)
        {
            currentAmbientAudioSource.volume = Mathf.Lerp(startVolume, 0, t / ambientFadeDuration);
            yield return null;
        }
        currentAmbientAudioSource.Stop();
        Destroy(currentAmbientAudioSource.gameObject); // Clean up old AudioSource after fade out
    }

    GameObject ambientObject = Instantiate(ambientAudioPrefab, attachTo.position, Quaternion.identity, attachTo);
    nextAmbientAudioSource = ambientObject.GetComponent<AudioSource>();
    nextAmbientAudioSource.clip = newTrack;
    nextAmbientAudioSource.volume = 0;
    nextAmbientAudioSource.pitch = targetPitch;
    nextAmbientAudioSource.spatialBlend = targetSpatialBlend;
    nextAmbientAudioSource.loop = loop;
    nextAmbientAudioSource.Play();

    for (float t = 0; t < ambientFadeDuration; t += Time.deltaTime)
    {
        nextAmbientAudioSource.volume = Mathf.Lerp(0, targetVolume, t / ambientFadeDuration);
        nextAmbientAudioSource.pitch = Mathf.Lerp(0, targetPitch, t / ambientFadeDuration);
        nextAmbientAudioSource.spatialBlend = Mathf.Lerp(0, targetSpatialBlend, t / ambientFadeDuration);
        yield return null;
    }

    nextAmbientAudioSource.volume = targetVolume;
    currentAmbientAudioSource = nextAmbientAudioSource;
    isFadingAmbientAudio = false; // Reset flag after fade completes
}
    #endregion
    // --------------------------------------------------------------------------------------------
    
    // --------------------------------------------------------------------------------------------
    #region StopAmbientAudio ------------------------------------
    public void StopAmbientAudio(float fadeDuration)
    {
        ambientFadeDuration = fadeDuration;
    
        // Check if there's ambient audio playing and that it's not already fading
        if (currentAmbientAudioSource != null && currentAmbientAudioSource.isPlaying && !isFadingAmbientAudio)
        {
            StartCoroutine(FadeOutCurrentAmbientAudio());
        }
    }

    private IEnumerator FadeOutCurrentAmbientAudio()
    {
        isFadingAmbientAudio = true;
        float startVolume = currentAmbientAudioSource.volume;

        // Fade out over ambientFadeDuration
        for (float t = 0; t < ambientFadeDuration; t += Time.deltaTime)
        {
            currentAmbientAudioSource.volume = Mathf.Lerp(startVolume, 0, t / ambientFadeDuration);
            yield return null;
        }

        // Stop and clean up the ambient audio source after fade-out
        currentAmbientAudioSource.Stop();
        Destroy(currentAmbientAudioSource.gameObject);
        currentAmbientAudioSource = null;  // Reset the currentAmbientAudioSource reference
        isFadingAmbientAudio = false; // Allow other fades to proceed
    }
    #endregion
    // --------------------------------------------------------------------------------------------
    
    // --------------------------------------------------------------------------------------------
    #region PauseAmbientAudio ------------------------------------
    public void PauseAmbientAudio(float fadeDuration)
    {
        // Check if a fade is already in progress to avoid interruptions
        if (isFadingAmbientAudio) return;

        ambientFadeDuration = fadeDuration; // Set the fade duration for pausing
    
        // Toggle pause state
        if (isPausedAmbientAudio)
        {
            // Resume the ambient audio with fade-in if currently paused
            StartCoroutine(FadeInAmbientAudio());
        }
        else
        {
            // Fade out and pause if currently playing
            StartCoroutine(FadeOutAndPauseAmbientAudio());
        }

        isPausedAmbientAudio = !isPausedAmbientAudio; // Toggle the pause state
    }

    private IEnumerator FadeOutAndPauseAmbientAudio()
    {
        isFadingAmbientAudio = true;
        float startVolume = currentAmbientAudioSource.volume;

        for (float t = 0; t < ambientFadeDuration; t += Time.deltaTime)
        {
            currentAmbientAudioSource.volume = Mathf.Lerp(startVolume, 0, t / ambientFadeDuration);
            yield return null;
        }

        currentAmbientAudioSource.Pause(); // Pause the ambient audio once fade-out completes
        isFadingAmbientAudio = false;
    }

    private IEnumerator FadeInAmbientAudio()
    {
        isFadingAmbientAudio = true;
        currentAmbientAudioSource.UnPause(); // Resume the ambient audio before fade-in
        float targetVolume = 1.0f; // Set to the desired full volume

        for (float t = 0; t < ambientFadeDuration; t += Time.deltaTime)
        {
            currentAmbientAudioSource.volume = Mathf.Lerp(0, targetVolume, t / ambientFadeDuration);
            yield return null;
        }

        currentAmbientAudioSource.volume = targetVolume; // Ensure final volume is set
        isFadingAmbientAudio = false;
    }
    #endregion
    // --------------------------------------------------------------------------------------------
    
    
    // --------------------------------------------------------------------------------------------
    #region PlaySoundEffects ------------------------------------
    public void PlaySoundEffect(Transform attachTo, string soundName, float volume, float pitch, bool randomizePitch, float pitchRange, float spatialBlend, string eventName)
    {
        // Check if the sound effect exists in the dictionary
        if (!soundEffects.TryGetValue(soundName, out AudioClip clip))
        {
            Debug.LogWarning($"Sound '{soundName}' not found in Resources/Audio/SFX!");
            return;
        }
        
        // If no transform is provided, play the sound at the AudioManager's position with no spatial blend
        if(attachTo == null)
        {
            attachTo = transform;
            spatialBlend = 0;
        }
        
        // Create a new GameObject to play the sound effect 
        GameObject sfxObject = Instantiate(soundEffectPrefab, attachTo.position, Quaternion.identity, attachTo);
        AudioSource sfxSource = sfxObject.GetComponent<AudioSource>();

        // Set the AudioSource properties and play the sound effect
        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.pitch = randomizePitch ? Random.Range(pitch - pitchRange, pitch + pitchRange) * pitch : pitch;
        sfxSource.spatialBlend = spatialBlend;
        sfxSource.Play();

        // Destroy the GameObject after the sound effect has finished playing
        Destroy(sfxObject, clip.length / sfxSource.pitch);
    }
    #endregion
    // --------------------------------------------------------------------------------------------
}
