using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component
    public AudioSource audioSource;       // Reference to the AudioSource component
    private string fullText;              // The full text to display
    private float clipLength;             // Length of the audio clip
    private float delayPerCharacter;      // Delay between each character
    private bool isTyping = false;        // Flag to indicate typing is in progress
    public float speedUpText = 1;

    void OnEnable()
    {
        // Get the TextMeshProUGUI component if not assigned
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        // Store the full text
        fullText = textComponent.text;

        // Set the text and hide all characters initially
        textComponent.text = fullText;
        textComponent.maxVisibleCharacters = 0;

        // Find the AudioSource component on a child object if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }

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

        // Prevent division by zero
        int textLength = Mathf.Max(fullText.Length, 1);
        delayPerCharacter = clipLength / textLength / speedUpText;

        // Start the typing coroutine
        isTyping = true;
        StartCoroutine(TypeText());
    }

    void Update()
    {
        // Skip typing effect and show full text immediately
        if (isTyping && Input.GetKeyDown(KeyCode.Space)) // Or your preferred input
        {
            isTyping = false;
            textComponent.maxVisibleCharacters = fullText.Length;
        }
    }

    IEnumerator TypeText()
    {
        int totalCharacters = fullText.Length;
        int visibleCount = 0;

        while (visibleCount <= totalCharacters && isTyping)
        {
            textComponent.maxVisibleCharacters = visibleCount;
            visibleCount++;

            // Optional: Play typing sound
            // if (typingSound != null)
            // {
            //     AudioSource.PlayClipAtPoint(typingSound, Camera.main.transform.position, 0.5f);
            // }

            yield return new WaitForSeconds(delayPerCharacter);
        }

        isTyping = false;
    }
}
