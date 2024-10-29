using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public float runningMin;
    public string prefix = "";
    public string suffix = "";
    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("North", false);
        anim.SetBool("South", false);
        anim.SetBool("East", false);
        anim.SetBool("West", false);
        if (rb.velocity == Vector2.zero) anim.SetBool("Moving", false);
        else anim.SetBool("Moving", true);
        if (rb.velocity.y >= runningMin || rb.velocity.y <= -runningMin || rb.velocity.x >= runningMin || rb.velocity.x <= -runningMin) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
        float x = Mathf.Abs(rb.velocity.x);
        float y = Mathf.Abs(rb.velocity.y);
        if(suffix != "Idle")
        {
            if(x > y)
            {
                if (rb.velocity.x > 0) anim.SetBool("East", true);
                else anim.SetBool("West", true);
            }
            else
            {
                if (rb.velocity.y > 0) anim.SetBool("North", true);
                else anim.SetBool("South", true);
            }
        }
        string clip = "Base Layer." + prefix + "." + suffix;
        if(!attacking)anim.Play(clip);
    }

    private void OnMouseDown()
    {
        Debug.Log("EST");
        string clip = "Attacks.AttackingS";
        anim.Play(clip);
    }
}
