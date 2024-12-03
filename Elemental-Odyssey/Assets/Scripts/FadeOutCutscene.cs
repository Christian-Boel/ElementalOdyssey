using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutCutscene : MonoBehaviour
{
    public Image imageToFade;         // Assign your UI Image here via the Inspector
    public float fadeDuration = 1f;   // Duration for fade-in and fade-out
    public float opaqueDelay = 2f;    // Duration to remain fully opaque
    public bool fade = false;         // Set this to true to fade in, false to fade out

    private Coroutine currentFadeCoroutine;
    private bool isOpaqueDelayActive = false;
    private bool fadeComplete = true; // Tracks whether fading is complete

    void Update()
    {
        // Start fading in if fade is true and image is not fully opaque
        if (fade && imageToFade.color.a < 1f && currentFadeCoroutine == null)
        {
            fadeComplete = false; // Fading process starts
            currentFadeCoroutine = StartCoroutine(FadeIn());
        }
        // Start fading out if fade is false and image is not fully transparent
        else if (!fade && imageToFade.color.a > 0f && currentFadeCoroutine == null && !isOpaqueDelayActive)
        {
            fadeComplete = false; // Fading process starts
            currentFadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    public bool IsFadeComplete()
    {
        return fadeComplete; // Returns whether fading is done
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(FadeImage(1f)); // Fade to alpha = 1
        isOpaqueDelayActive = true;
        yield return new WaitForSeconds(opaqueDelay); // Wait while fully opaque
        isOpaqueDelayActive = false;

        // Automatically start fading out if fade is still true
        if (fade)
        {
            fade = false;
        }

        fadeComplete = true; // Fade-in process is complete
    }

    IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeImage(0f)); // Fade to alpha = 0
        fadeComplete = true; // Fade-out process is complete
        currentFadeCoroutine = null; // Reset the coroutine tracker
    }

    IEnumerator FadeImage(float targetAlpha)
    {
        float startAlpha = imageToFade.color.a;
        float elapsedTime = 0f;
        Color imageColor = imageToFade.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            imageToFade.color = new Color(imageColor.r, imageColor.g, imageColor.b, currentAlpha);
            yield return null; // Wait for the next frame
        }

        // Ensure the final alpha value is set
        imageToFade.color = new Color(imageColor.r, imageColor.g, imageColor.b, targetAlpha);

        currentFadeCoroutine = null; // Reset the coroutine tracker
    }
}
