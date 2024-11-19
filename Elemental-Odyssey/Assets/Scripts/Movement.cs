using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IAttackedPossible
{

    [SerializeField] private float MSSpeed = 5f;
    private Rigidbody2D rb;
    /*[HideInInspector]*/ public bool attacking;
    public float attackPenalty = .3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(input.magnitude > 1)
        {
            input = input.normalized;
        }

        float attackSlowdown = attacking ? attackPenalty : 1f;

        rb.velocity =  MSSpeed * attackSlowdown * input;
    }

    public void takeDmg(float dmg)
    {

    }
}
