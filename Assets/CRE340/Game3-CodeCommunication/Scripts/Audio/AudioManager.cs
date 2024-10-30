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

    private void OnEnable()
    {
        AudioEventManager.PlayBGM += PlayMusic;
        AudioEventManager.PlaySFX += PlaySoundEffect;
    }

    private void OnDisable()
    {
        AudioEventManager.PlayBGM -= PlayMusic;
        AudioEventManager.PlaySFX -= PlaySoundEffect;
    }

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

    public void PlaySoundEffect(Transform attachTo, string soundName, float volume, float pitch, bool randomizePitch, float pitchRange, float spatialBlend)
    {
        if (!soundEffects.TryGetValue(soundName, out AudioClip clip))
        {
            Debug.LogWarning($"Sound '{soundName}' not found in Resources/Audio/SFX!");
            return;
        }

        GameObject sfxObject = Instantiate(soundEffectPrefab, attachTo.position, Quaternion.identity, attachTo);
        AudioSource sfxSource = sfxObject.GetComponent<AudioSource>();

        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.pitch = randomizePitch ? Random.Range(pitch - pitchRange, pitch + pitchRange) * pitch : pitch;
        sfxSource.spatialBlend = spatialBlend;
        sfxSource.Play();

        Destroy(sfxObject, clip.length / sfxSource.pitch);
    }
}
