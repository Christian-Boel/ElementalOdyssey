using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.5f;
    [SerializeField] private bool loopMusic = true;

    private AudioSource musicAudioSource;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            musicAudioSource = gameObject.AddComponent<AudioSource>();
            musicAudioSource.playOnAwake = false;
            musicAudioSource.loop = loopMusic;
            musicAudioSource.volume = musicVolume;
            if (backgroundMusicClip != null)
            {
                PlayMusic(backgroundMusicClip);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip == null)
            return;

        if (musicAudioSource.isPlaying && musicAudioSource.clip == musicClip)
            return;

        musicAudioSource.clip = musicClip;
        musicAudioSource.volume = musicVolume;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = null;
        }
    }

    public void SetVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicAudioSource.volume = musicVolume;
    }

    public void FadeOutMusic(float duration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = musicAudioSource.volume;

        while (musicAudioSource.volume > 0)
        {
            musicAudioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicAudioSource.Stop();
        musicAudioSource.volume = musicVolume;
    }

    public void FadeInMusic(AudioClip newClip, float duration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        musicAudioSource.clip = newClip;
        musicAudioSource.volume = 0;
        musicAudioSource.Play();

        fadeCoroutine = StartCoroutine(FadeInCoroutine(duration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        while (musicAudioSource.volume < musicVolume)
        {
            musicAudioSource.volume += musicVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicAudioSource.volume = musicVolume;
    }

    public void FadeToMusic(AudioClip newClip, float duration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToMusicCoroutine(newClip, duration));
    }

    private IEnumerator FadeToMusicCoroutine(AudioClip newClip, float duration)
    {
        float startVolume = musicAudioSource.volume;

        // Fade out current music
        while (musicAudioSource.volume > 0)
        {
            musicAudioSource.volume -= startVolume * Time.deltaTime / (duration / 2);
            yield return null;
        }

        musicAudioSource.Stop();
        musicAudioSource.clip = newClip;
        musicAudioSource.Play();

        // Fade in new music
        while (musicAudioSource.volume < musicVolume)
        {
            musicAudioSource.volume += musicVolume * Time.deltaTime / (duration / 2);
            yield return null;
        }

        musicAudioSource.volume = musicVolume;
    }
}
