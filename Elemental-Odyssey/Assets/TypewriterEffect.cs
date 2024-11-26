using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;       // Reference to the Text component
    private string fullText;         // The full text to display
    private AudioSource audioSource; // Reference to the AudioSource component
    private float clipLength;        // Length of the audio clip
    private float delayPerCharacter; // Delay between each character

    void OnEnable()
    {
        // Get the Text component if not assigned
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        // Store the full text and clear the text component
        fullText = textComponent.text;
        textComponent.text = "";

        // Find the AudioSource component on a child object
        audioSource = GetComponentInChildren<AudioSource>();

        if (audioSource != null && audioSource.clip != null)
        {
            // Get the length of the audio clip
            clipLength = audioSource.clip.length;
        }
        else
        {
            // If no audio clip is found, set a default duration
            clipLength = fullText.Length * 0.05f; // For example, 0.05 seconds per character
        }

        // Calculate the delay between characters
        delayPerCharacter = clipLength / fullText.Length;

        // Start the typing coroutine
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delayPerCharacter);
        }
    }
}
