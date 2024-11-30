using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance; // Singleton
    
    [SerializeField] private AudioSource soundFXObject; // Prefab for sound effects

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Optionally persist across scenes
            // DontDestroyOnLoad(gameObject);
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

        // Assign the audio clip and volume
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Destroy the audio source after the clip has finished playing
        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips provided to PlayRandomSoundFXClip.");
            return;
        }

        // Select a random clip and play it
        int rand = Random.Range(0, audioClips.Length);
        PlaySoundFXClip(audioClips[rand], spawnTransform, volume);
    }
}