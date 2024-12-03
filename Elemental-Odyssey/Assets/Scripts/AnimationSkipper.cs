using UnityEngine;
using UnityEngine.UI;

public class AnimationSkipper : MonoBehaviour
{
    public Animator animator;                             // Reference to the Animator component
    public string animationState;                         // Name of the animation state
    public SpriteKeyframeCollection keyframeCollection;   // The keyframe data collected earlier
    public KeyCode skipKey = KeyCode.Space;               // Key to trigger the skip
    public MonoBehaviour destroyScript;                   // Reference to the DestroyScript component

    private AnimationClip clip;                           // Reference to the animation clip
    private float clipLength;                             // Length of the clip in seconds


    private bool playerDisables = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // Get the AnimationClip associated with the animation state
        clip = GetAnimationClip(animator, animationState);

        if (clip != null)
        {
            clipLength = clip.length;
        }
        else
        {
            Debug.LogError($"Animation clip '{animationState}' not found.");
        }

        // Ensure the DestroyScript is disabled at the start
        if (destroyScript != null)
            destroyScript.enabled = false;
    }

    void Update()
    {
        // if (GameObject.FindGameObjectWithTag("Player") && !playerDisables)
        // {
        //     playerDisables = true;
        //     GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject.SetActive(false);
        // }
        // Check for key input to trigger the skip
        if (Input.GetKeyDown(skipKey))
        {
            SkipToNextSpriteKey();
        }
    }

    // Method to skip to the next sprite keyframe or enable DestroyScript if on the last keyframe
    void SkipToNextSpriteKey()
    {
        if (clip == null || keyframeCollection == null)
        {
            Debug.LogWarning("Clip or keyframe collection is not assigned.");
            return;
        }

        // Get the current normalized time of the animation state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = stateInfo.normalizedTime % 1f;
        float currentTime = currentNormalizedTime * clipLength;

        SpriteKeyframeData[] keyframes = keyframeCollection.keyframes;

        // Find the index of the current sprite keyframe
        int currentKeyIndex = GetCurrentSpriteKeyIndex(currentTime);

        if (currentKeyIndex == keyframes.Length - 1)
        {
            // We are on the last keyframe
            if (destroyScript != null)
            {
                destroyScript.enabled = true;
            }
        }
        else
        {
            // Not on last keyframe, skip to next keyframe
            int nextKeyIndex = currentKeyIndex + 1;
            float nextKeyTime = keyframes[nextKeyIndex].time;

            // Calculate normalized time
            float normalizedTime = nextKeyTime / clipLength;

            // Play the animation state at the specified normalized time
            animator.Play(animationState, -1, normalizedTime);

            // Force the Animator to update immediately
            animator.Update(0f);
        }
    }

    // Helper method to retrieve the AnimationClip by state name
    AnimationClip GetAnimationClip(Animator animator, string stateName)
    {
        // Iterate through all animation clips in the Animator Controller
        foreach (AnimationClip ac in animator.runtimeAnimatorController.animationClips)
        {
            if (ac.name == stateName)
            {
                return ac;
            }
        }
        return null;
    }

    // Method to get the index of the current sprite keyframe
    int GetCurrentSpriteKeyIndex(float currentTime)
    {
        SpriteKeyframeData[] keyframes = keyframeCollection.keyframes;

        for (int i = keyframes.Length - 1; i >= 0; i--)
        {
            if (currentTime >= keyframes[i].time)
            {
                return i;
            }
        }
        return 0; // Before the first keyframe
    }
}
