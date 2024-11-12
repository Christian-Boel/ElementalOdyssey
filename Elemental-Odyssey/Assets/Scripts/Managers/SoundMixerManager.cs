using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", MathF.Log10(level)*20f); //db er ikke linear men logaritmisk. Derfor denne udregning 
    }

    public void SetSoundFXVolume (float level)
    {
        audioMixer.SetFloat("soundFXVolume", MathF.Log10(level)*20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", MathF.Log10(level)*20f);
    }
}
