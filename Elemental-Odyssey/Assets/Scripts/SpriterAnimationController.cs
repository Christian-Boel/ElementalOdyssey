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

        animator = spriter.Animator;

        if (animator == null)
        {
            Debug.LogError("Animator not found on SpriterDotNetBehaviour!");
        }
    }

    public void PlayAnimation(string animName)
    {
        if (animator == null) Debug.LogError("Animator not Found");
        animator.Play(animName);
    }

}
