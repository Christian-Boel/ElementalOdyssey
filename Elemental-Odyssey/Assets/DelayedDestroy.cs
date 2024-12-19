using System.Collections;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to destroy the object after 2 seconds
        StartCoroutine(DestroyObjectAfterDelay(2f));
    }

    // Coroutine to destroy the object after a delay
    private IEnumerator DestroyObjectAfterDelay(float delay)
    {
        // Wait for the specified amount of time (in seconds)
        yield return new WaitForSeconds(delay);

        // Destroy the object this script is attached to
        Destroy(gameObject);
    }
}
