using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource; // The AudioSource component that will play the music

    // Method to play a music clip
    public void PlayMusic(AudioClip musicClip)
    {
        // If there's already music playing, stop it before switching to a new track
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        // Set the new clip and play it
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    // Method to stop the music
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop(); // Stops the current music
        }
    }

    // Optional: Method to pause the music
    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause(); // Pauses the current music without stopping it
        }
    }

    // Optional: Method to resume the music
    public void ResumeMusic()
    {
        if (!musicSource.isPlaying && musicSource.clip != null)
        {
            musicSource.UnPause(); // Resumes the paused music
        }
    }
}
