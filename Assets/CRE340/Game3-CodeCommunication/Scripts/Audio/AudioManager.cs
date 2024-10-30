using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music Settings")]
    public GameObject musicPrefab;
    private float musicFadeDuration = 1.5f;
    private FadeType musicFadeType = FadeType.Crossfade;
    private bool isFading = false; // Flag to prevent multiple fades at once
    private bool isPaused = false; // Tracks if the music is paused

    private Dictionary<int, AudioClip> musicTracks = new Dictionary<int, AudioClip>();
    private AudioSource currentMusicSource;
    private AudioSource nextMusicSource;

    [Header("Sound Effects Settings")]
    public GameObject soundEffectPrefab;
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    [Header("Available Music Tracks")]
    [SerializeField] private List<string> musicTrackNames = new List<string>();

    [Header("Available Sound Effects")]
    [SerializeField] private List<string> soundEffectNames = new List<string>();

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
        AudioEventManager.PlaySFX += PlaySoundEffect;
    }

    private void OnDisable()
    {
        AudioEventManager.PlayBGM -= PlayMusic;
        AudioEventManager.StopBGM -= StopMusic;
        AudioEventManager.PauseBGM -= PauseMusic;
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
    public void PlayMusic(int trackNumber, string trackName, float volume, FadeType fadeType, float fadeDuration, bool loop = true)
    {
        if (isFading) return; // Block if a fade/crossfade is already in progress

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
    public void PlayMusic(int trackNumber, float volume = 1.0f, bool loop = true)
    {
        if (isFading) return; // Block if a fade/crossfade is already in progress

        if (!musicTracks.TryGetValue(trackNumber, out AudioClip newTrack)) return;
        isFading = true;
        if (musicFadeType == FadeType.Crossfade)
        {
            StartCoroutine(CrossfadeMusic(newTrack, volume, loop));
        }
        else
        {
            StartCoroutine(FadeOutAndInMusic(newTrack, volume, loop));
        }
    }

    public void PlayMusic(string trackName, float volume = 1.0f, bool loop = true)
    {
        if (isFading) return; // Block if a fade/crossfade is already in progress

        foreach (var track in musicTracks)
        {
            if (track.Value.name == trackName)
            {
                isFading = true;
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
        isFading = false; // Reset flag after crossfade completes
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
        isFading = false; // Reset flag after fade completes
    }
    #endregion
    // --------------------------------------------------------------------------------------------


    // --------------------------------------------------------------------------------------------
    #region StopBackgroundMusic ------------------------------------
    public void StopMusic(float fadeDuration)
    {
        musicFadeDuration = fadeDuration;
        
        // Check if there's music playing and that it's not already fading
        if (currentMusicSource != null && currentMusicSource.isPlaying && !isFading)
        {
            StartCoroutine(FadeOutCurrentMusic());
        }
    }

    private IEnumerator FadeOutCurrentMusic()
    {
        isFading = true;
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
        isFading = false; // Allow other fades to proceed
    }
    #endregion
    // --------------------------------------------------------------------------------------------

    // --------------------------------------------------------------------------------------------
    #region PauseBackgroundMusic ------------------------------------
    public void PauseMusic(float fadeDuration)
    {
        // Check if a fade is already in progress to avoid interruptions
        if (isFading) return;

        musicFadeDuration = fadeDuration; // Set the fade duration for pausing
        
        // Toggle pause state
        if (isPaused)
        {
            // Resume the music with fade-in if currently paused
            StartCoroutine(FadeInMusic());
        }
        else
        {
            // Fade out and pause if currently playing
            StartCoroutine(FadeOutAndPauseMusic());
        }

        isPaused = !isPaused; // Toggle the pause state
    }
    private IEnumerator FadeOutAndPauseMusic()
    {
        isFading = true;
        float startVolume = currentMusicSource.volume;

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            currentMusicSource.volume = Mathf.Lerp(startVolume, 0, t / musicFadeDuration);
            yield return null;
        }

        currentMusicSource.Pause(); // Pause the music once fade-out completes
        isFading = false;
    }
    private IEnumerator FadeInMusic()
    {
        isFading = true;
        currentMusicSource.UnPause(); // Resume the music before fade-in
        float targetVolume = 1.0f; // Set to the desired full volume

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            currentMusicSource.volume = Mathf.Lerp(0, targetVolume, t / musicFadeDuration);
            yield return null;
        }

        currentMusicSource.volume = targetVolume; // Ensure final volume is set
        isFading = false;
    }

    #endregion
    // --------------------------------------------------------------------------------------------
    
    
    // --------------------------------------------------------------------------------------------
    #region PlaySoundEffects ------------------------------------
    public void PlaySoundEffect(Transform attachTo, string soundName, float volume, float pitch, bool randomizePitch, float pitchRange, float spatialBlend)
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
