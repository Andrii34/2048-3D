using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// Central audio manager for the game.
/// Responsibilities:
/// 1. Stores global volume for each AudioType.
/// 2. Plays SFX using a pool of AudioSources for performance.
/// 3. Provides dedicated AudioSources for Music and UI.
/// 4. Emits reactive volume observables so AudioEmitters can automatically respond to global volume changes.
/// 5. Provides a single point to set global volume, ensuring encapsulation and avoiding direct changes by objects.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private int sfxPoolSize = 10;

    // Global volume per audio type
    private readonly Dictionary<AudioType, ReactiveProperty<float>> _volumes = new()
    {
        { AudioType.SFX, new ReactiveProperty<float>(1f) },
        { AudioType.Music, new ReactiveProperty<float>(1f) },
        { AudioType.UI, new ReactiveProperty<float>(1f) }
    };

    // SFX pool
    private Queue<AudioSource> _sfxPool = new();

    // Persistent AudioSources for Music and UI
    private AudioSource _musicSource;
    private AudioSource _uiSource;

    private void Awake()
    {
        // Initialize SFX pool
        for (int i = 0; i < sfxPoolSize; i++)
        {
            var go = new GameObject($"SFXPool_{i}");
            go.transform.parent = transform;
            var source = go.AddComponent<AudioSource>();
            go.SetActive(false);
            _sfxPool.Enqueue(source);
        }

        // Initialize persistent AudioSources
        _musicSource = CreatePersistentSource("MusicSource");
        _uiSource = CreatePersistentSource("UISource");
    }

    private AudioSource CreatePersistentSource(string name)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go.AddComponent<AudioSource>();
    }

    /// <summary>
    /// Returns a reactive read-only property for global volume.
    /// AudioEmitters subscribe to this to automatically adjust their volume.
    /// </summary>
    public IReadOnlyReactiveProperty<float> GetVolumeObservable(AudioType type)
    {
        return _volumes[type];
    }

    /// <summary>
    /// Set the global volume for a specific audio type.
    /// Only UI or centralized controllers should call this.
    /// </summary>
    public void SetVolume(AudioType type, float volume)
    {
        _volumes[type].Value = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Play an audio clip of a specific type.
    /// Uses SFX pool for SFX, persistent AudioSources for Music/UI.
    /// </summary>
    public void PlaySound(AudioType type, AudioClip clip, Transform parent = null, float emitterVolume = 1f)
    {
        switch (type)
        {
            case AudioType.SFX:
                PlaySFX(clip, parent, emitterVolume);
                break;
            case AudioType.Music:
                PlayPersistent(_musicSource, clip, emitterVolume);
                break;
            case AudioType.UI:
                PlayPersistent(_uiSource, clip, emitterVolume);
                break;
        }
    }

    private void PlaySFX(AudioClip clip, Transform parent, float emitterVolume)
    {
        AudioSource source = _sfxPool.Count > 0 ? _sfxPool.Dequeue() : new GameObject("SFXExtra").AddComponent<AudioSource>();

        source.gameObject.SetActive(true);
        if (parent != null) source.transform.position = parent.position;

        source.clip = clip;
        source.volume = _volumes[AudioType.SFX].Value * emitterVolume;
        source.Play();

        // Return source to pool after clip ends
        Observable.Timer(System.TimeSpan.FromSeconds(clip.length))
            .Subscribe(_ =>
            {
                source.gameObject.SetActive(false);
                source.transform.parent = transform;
                _sfxPool.Enqueue(source);
            }).AddTo(this);
    }

    private void PlayPersistent(AudioSource source, AudioClip clip, float emitterVolume)
    {
        source.clip = clip;
        source.volume = _volumes[clip ? AudioType.Music : AudioType.UI].Value * emitterVolume;
        source.Play();
    }
}


public enum AudioType 
{ 
    SFX,
    Music,
    UI,
}


