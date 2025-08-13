using UnityEngine;
using UniRx;
using Zenject;

/// <summary>
/// Local component for objects that play audio.
/// Responsibilities:
/// 1. Holds a reference to AudioManager to play sounds.
/// 2. Maintains local volume (EmitterVolume) for this object.
/// 3. Subscribes to global volume changes for its AudioType using UniRx.
/// 4. Provides a simple Play() interface for the object.
/// Usage:
/// - Place on objects with persistent AudioSource (e.g., Music, UI, ambient objects)
/// - Optional for short SFX if you call AudioManager directly
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioEmitter : MonoBehaviour
{
    [SerializeField] private AudioType _type;
    [Range(0f, 1f)] public float EmitterVolume = 1f;

    private AudioManager _audioManager;
    private AudioSource _audioSource;

    [Inject]
    public void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Subscribe to global volume updates
        _audioManager.GetVolumeObservable(_type)
            .Subscribe(globalVolume => UpdateVolume(globalVolume))
            .AddTo(this);
    }

    private void UpdateVolume(float globalVolume)
    {
        // Apply combined volume (global * local)
        _audioSource.volume = globalVolume * EmitterVolume;
    }

    /// <summary>
    /// Plays a clip through AudioManager, taking into account type and emitter volume.
    /// </summary>
    public void Play(AudioClip clip)
    {
        _audioManager.PlaySound(_type, clip, transform, EmitterVolume);
    }
}

