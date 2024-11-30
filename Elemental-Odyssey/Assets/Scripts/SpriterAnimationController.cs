using UnityEngine;
using SpriterDotNetUnity;

public class SpriterAnimationController : MonoBehaviour
{
    private SpriterDotNetBehaviour spriter;
    private UnityAnimator animator;

    void Start()
    {
        spriter = GetComponent<SpriterDotNetBehaviour>();

        if (spriter == null)
        {
            Debug.LogError("SpriterDotNetBehaviour component not found!");
            return;
        }

        // Wait until Animator is initialized
        animator = spriter.Animator;

        if (animator == null)
        {
            Debug.LogWarning("Animator is not yet initialized. Waiting for initialization...");
        }
    }

    void Update()
    {
        // Retry initialization if the Animator was not ready during Start
        if (animator == null && spriter.Animator != null)
        {
            animator = spriter.Animator;
            Debug.LogWarning("Animator Initialized");
        }
    }

    public void PlayAnimation(string animName)
    {
        if (animator == null)
        {
            Debug.LogError("Animator not initialized yet.");
            return;
        }

        try
        {
            try
            {
                animator.Play(animName);
            }
            catch
            {
                animator.Play(animName.ToLower());
            }
        }
        catch
        {
            Debug.LogError("No animation found for" + animName);
        }
    }
}
