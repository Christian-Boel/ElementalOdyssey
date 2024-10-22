using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    public AnimationClip[] clips;
    private char facing = 'S';
    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            attacking = true;
            switch(facing)
            {
                case 'E':
                    animator.Play(clips[4].name);
                    break;
                case 'A':
                    animator.Play(clips[5].name);
                    break;
                case 'S':
                    animator.Play(clips[6].name);
                    break;
                case 'N':
                    animator.Play(clips[7].name);
                    break;
            }
        }
        else
        {
            attacking = false;
        }
        if(Input.GetKey(KeyCode.D) && !attacking)
        {
            animator.Play(clips[0].name);
            facing = 'E';
        }
        else if (Input.GetKey(KeyCode.A) && !attacking)
        {
            animator.Play(clips[1].name);
            facing = 'W';
        }
        else if (Input.GetKey(KeyCode.S) && !attacking)
        {
            Debug.Log("Playing S animation");
            animator.Play(clips[2].name);
            facing = 'S';
        }
        else if (Input.GetKey(KeyCode.W) && !attacking)
        {
            animator.Play(clips[3].name);
            facing = 'N';
        }
    }
}
