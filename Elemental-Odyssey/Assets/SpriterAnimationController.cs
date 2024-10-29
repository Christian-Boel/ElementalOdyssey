using UnityEngine;
using SpriterDotNetUnity;

public class SpriterAnimationController : MonoBehaviour
{
    private SpriterDotNetBehaviour spriter;
    public Animator animator;
    void Start()
    {
        spriter = GetComponent<SpriterDotNetBehaviour>();
        animator = spriter.GetComponent<Animator>();

        if (spriter == null)
        {
            Debug.LogError("SpriterDotNetBehaviour component not found!");
            return;
        }

        Debug.Log("Available Animations:");
        foreach (var anim in spriter.Animator.GetAnimations())
        {
            Debug.Log(anim);
        }

        if (spriter.Animator.HasAnimation("ATTACK"))
        {
            spriter.Animator.Play("ATTACK");
            Debug.Log("Playing ATTACK animation.");
        }
        else
        {
            Debug.LogError("ATTACK animation not found!");
        }
    }

}
