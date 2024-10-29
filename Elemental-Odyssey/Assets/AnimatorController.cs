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
        anim.Play("Base Layer.South.Idle", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector2.zero) suffix = "Idle";
        else if (rb.velocity.y >= runningMin || rb.velocity.y <= -runningMin || rb.velocity.x >= runningMin || rb.velocity.x <= -runningMin) suffix = "Run";
        else suffix = "Walking";
        float x = Mathf.Abs(rb.velocity.x);
        float y = Mathf.Abs(rb.velocity.y);
        if(suffix != "Idle")
        {
            if(x > y)
            {
                if (rb.velocity.x > 0) prefix = "East";
                else prefix = "West";
            }
            else
            {
                if (rb.velocity.y > 0) prefix = "North";
                else prefix = "South";
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
