using Torretas;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip levelMusic;

    [Header("Volume")]
    [SerializeField, Range(0f, 1f)]
    private float musicVolume = 0.5f;

    [SerializeField, Range(0f, 1f)]
    private float sfxVolume = 1f;

    private void Awake()
    {
        musicSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.spatialBlend = 0f;
        musicSource.volume = musicVolume;

        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;
        sfxSource.volume = sfxVolume;
    }

    private void OnEnable()
    {
        Turret.OnSfxRequested += PlaySfx;
    }

    private void OnDisable()
    {
        Turret.OnSfxRequested -= PlaySfx;
    }

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (levelMusic == null) return;

        musicSource.clip = levelMusic;
        musicSource.Play();
    }

    private void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }
}
