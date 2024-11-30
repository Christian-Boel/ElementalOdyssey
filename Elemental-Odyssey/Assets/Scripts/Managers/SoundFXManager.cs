using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance; //singleton 
    [Header("Audio Source Prefab for Sound Effects")]
    [SerializeField] private AudioSource soundFXObject; // Prefab for sound effects

    [Header("Audio Source for Background Music")]
    [SerializeField] private AudioSource musicAudioSource; // Add this line

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip == null)
        {
            Debug.LogWarning("No audio clip provided to PlaySoundFXClip.");
            return;
        }

        // Spawn an audio source object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the audio clip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Get length of the sound FX clip
        float clipLength = audioClip.length;

        // Destroy the audio source after the clip has finished playing
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips provided to PlayRandomSoundFXClip.");
            return;
        }

        // Assign a random index
        int rand = UnityEngine.Random.Range(0, audioClips.Length);

        // Spawn an audio source object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign the audio clip
        audioSource.clip = audioClips[rand];

        // Assign volume
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Get length of the sound FX clip
        float clipLength = audioSource.clip.length;

        // Destroy the audio source after the clip has finished playing
        Destroy(audioSource.gameObject, clipLength);
    }
    
    public void PlayBackgroundMusic(AudioClip musicClip, float volume = 1f, bool loop = true)
    {
        if (musicClip == null)
        {
            Debug.LogWarning("No audio clip provided to PlayBackgroundMusic.");
            return;
        }

        if (musicAudioSource.clip == musicClip && musicAudioSource.isPlaying)
        {
            // Already playing this music
            return;
        }

        musicAudioSource.clip = musicClip;
        musicAudioSource.volume = volume;
        musicAudioSource.loop = loop;
        musicAudioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicAudioSource.Stop();
    }

    public void PauseBackgroundMusic()
    {
        musicAudioSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        musicAudioSource.UnPause();
    }
}
