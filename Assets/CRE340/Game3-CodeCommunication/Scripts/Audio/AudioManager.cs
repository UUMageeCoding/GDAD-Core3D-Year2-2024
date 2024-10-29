using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music Settings")]
    public AudioSource musicSource;
    public float musicFadeDuration = 1.5f;
    private Dictionary<int, AudioClip> musicTracks = new Dictionary<int, AudioClip>();

    [Header("Sound Effects Settings")]
    public GameObject soundEffectPrefab;  // Prefab with AudioSource component
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();
    public float randomPitchMin = 0.9f;
    public float randomPitchMax = 1.1f;

    [Header("Available Sound Effects")]
    [SerializeField] private List<string> soundEffectNames = new List<string>();  // List of SFX names for inspector display

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

    /// <summary>
    /// Loads audio resources from Resources/Audio/BGM for music and Resources/Audio/SFX for sound effects.
    /// </summary>
    private void LoadAudioResources()
    {
        // Load background music
        AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Audio/BGM");
        for (int i = 0; i < bgmClips.Length; i++)
        {
            musicTracks[i] = bgmClips[i];
        }

        // Load sound effects and populate names list
        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Audio/SFX");
        foreach (var clip in sfxClips)
        {
            soundEffects[clip.name] = clip;
            soundEffectNames.Add(clip.name);  // Add the name to the list
        }
    }

    /// <summary>
    /// Populates the list of sound effect names for inspector display.
    /// </summary>
    public void UpdateSoundEffectNames()
    {
        soundEffectNames.Clear();
        foreach (var clipName in soundEffects.Keys)
        {
            soundEffectNames.Add(clipName);
        }
    }

    /// <summary>
    /// Plays background music based on track number with a fade effect.
    /// </summary>
    public void PlayMusic(int trackNumber)
    {
        if (!musicTracks.TryGetValue(trackNumber, out AudioClip newTrack)) return;

        StartCoroutine(FadeMusic(newTrack));
    }

    private IEnumerator FadeMusic(AudioClip newTrack)
    {
        if (musicSource.isPlaying)
        {
            // Fade out the current track
            float startVolume = musicSource.volume;
            for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVolume, 0, t / musicFadeDuration);
                yield return null;
            }
            musicSource.Stop();
        }

        // Play and fade in the new track
        musicSource.clip = newTrack;
        musicSource.Play();

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, 1, t / musicFadeDuration);
            yield return null;
        }
    }

    /// <summary>
    /// Plays a sound effect attached to a specific transform.
    /// </summary>
    public void PlaySoundEffect(Transform attachTo, string soundName, float volume = 1.0f, float pitch = 1.0f, bool randomizePitch = false)
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
        sfxSource.pitch = randomizePitch ? Random.Range(randomPitchMin, randomPitchMax) * pitch : pitch;
        sfxSource.Play();

        Destroy(sfxObject, clip.length / sfxSource.pitch);  // Destroy after playback completes
    }
}
