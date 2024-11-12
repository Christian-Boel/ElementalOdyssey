using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private enum DirectionState { North, South, East, West}
    [SerializeField] private DirectionState directionState = DirectionState.South;

    [SerializeField] private enum MovementState {Idle, Walking, Running}
    [SerializeField] private MovementState movementState = MovementState.Idle;

    public Vector2 velocity;
    private Animator anim;
    public float runningMin;
    private Rigidbody2D rb;

    private float movingAngle;
    public float speed;

    private float lastEventTime;
    public float idleTime = 30f;
    private bool superIdle = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastEventTime = 0;
    }

    private void UpdateMovementState()
    {
        speed = velocity.magnitude;
        switch(movementState)
        {
            case MovementState.Idle:
                lastEventTime += Time.deltaTime;
                if (speed >= runningMin)
                {
                    movementState = MovementState.Running;
                    anim.SetBool("Moving", true);
                    anim.SetBool("Running", true);
                    superIdle = false;
                    lastEventTime = 0;
                }
                else if(speed != 0)
                {
                    movementState = MovementState.Walking;
                    anim.SetBool("Moving", true);
                    anim.SetBool("Running", false);
                    superIdle = false;
                    lastEventTime = 0;
                }
                break;
            case MovementState.Walking:
                if (speed >= runningMin)
                {
                    movementState = MovementState.Running;
                    anim.SetBool("Moving", true);
                    anim.SetBool("Running", true);
                }
                else if (speed == 0)
                {
                    movementState = MovementState.Idle;
                    anim.SetBool("Moving", false);
                    anim.SetBool("Running", false);
                }
                break;
            case MovementState.Running:
                if(speed == 0)
                {
                    movementState = MovementState.Idle;
                    anim.SetBool("Moving", false);
                    anim.SetBool("Running", false);
                }
                else if (speed < runningMin)
                {
                    movementState = MovementState.Walking;
                    anim.SetBool("Moving", true);
                    anim.SetBool("Running", false);
                }
                break;
        }
        if(lastEventTime >= idleTime && !superIdle)
        {
            superIdle = true;
            anim.Play("Base Layer.SuperIdle");
        }
    }

    private void UpdateDirectionState()
    {
        
        switch(directionState)
        {
            case DirectionState.North:
                if (movingAngle >= -45 && movingAngle < 45)
                {
                    directionState = DirectionState.East;
                    anim.SetBool("North", false);
                    anim.SetBool("South", false);
                    anim.SetBool("East", true);
                    anim.SetBool("West", false);
                }
                else if (movingAngle >= 135 || movingAngle < -135)
                {
                    directionState = DirectionState.West;
                    anim.SetBool("North", false);
                    anim.SetBool("South", false);
                    anim.SetBool("East", false);
                    anim.SetBool("West", true);
                }
                else if (movingAngle >= -135 && movingAngle < -45)
                {
                    directionState = DirectionState.South;
                    anim.SetBool("North", false);
                    anim.SetBool("South", true);
                    anim.SetBool("East", false);
                    anim.SetBool("West", false);
                }
                break;
            case DirectionState.South:

                if (movingAngle >= -45 && movingAngle < 45)
                {
                    directionState = DirectionState.East;
                    anim.SetBool("North", false);
                    anim.SetBool("South", false);
                    anim.SetBool("East", true);
                    anim.SetBool("West", false);
                }
                else if (movingAngle >= 45 && movingAngle < 135)
                {
                    directionState = DirectionState.North;
                    anim.SetBool("North", true);
                    anim.SetBool("South", false);
                    anim.SetBool("East", false);
                    anim.SetBool("West", false);
                }
                else if (movingAngle >= 135 || movingAngle < -135)
                {
                    directionState = DirectionState.West;
                    anim.SetBool("North", false);
                    anim.SetBool("South", false);
                    anim.SetBool("East", false);
                    anim.SetBool("West", true);
                }
                break;
            case DirectionState.West:

                if (movingAngle >= -45 && movingAngle < 45)
                {
                    directionState = DirectionState.East;
                    anim.SetBool("North", false);
                    anim.SetBool("South", false);
                    anim.SetBool("East", true);
                    anim.SetBool("West", false);
                }
                else if (movingAngle >= 45 && movingAngle < 135)
                {
                    directionState = DirectionState.North;
                    anim.SetBool("North", true);
                    anim.SetBool("South", false);
                    anim.SetBool("East", false);
                    anim.SetBool("West", false);
                }
                else if (movingAngle >= -135 && movingAngle < -45)
                {
                    directionState = DirectionState.South;
                    anim.SetBool("North", false);
                    anim.SetBool("South", true);
                    anim.SetBool("East", false);
                    anim.SetBool("West", false);
                }
                break;
            case DirectionState.East:
                if (movingAngle >= 45 && movingAngle < 135)
                {
                    directionState = DirectionState.North;
                    anim.SetBool("North", true);
                    anim.SetBool("South", false);
                    anim.SetBool("East", false);
                    anim.SetBool("West", false);
                }
                else if (movingAngle >= 135 || movingAngle < -135)
                {
                    directionState = DirectionState.West;
                    anim.SetBool("North", false);
                    anim.SetBool("South", false);
                    anim.SetBool("East", false);
                    anim.SetBool("West", true);
                }
                else if (movingAngle >= -135 && movingAngle < -45)
                {
                    directionState = DirectionState.South;
                    anim.SetBool("North", false);
                    anim.SetBool("South", true);
                    anim.SetBool("East", false);
                    anim.SetBool("West", false);
                }
                break;
        }
    }

    public void UpdateAttackState(float attackAngle)
    {
        if (attackAngle >= -45 && attackAngle < 45)
        {
            anim.Play("Base Layer.Attacking.AttackingE");
        }
        else if (attackAngle >= 45 && attackAngle < 135)
        {
            anim.Play("Base Layer.Attacking.AttackingN");
        }
        else if (attackAngle >= 135 || attackAngle < -135)
        {
            anim.Play("Base Layer.Attacking.AttackingW");
        }
        else if (attackAngle >= -135 && attackAngle < -45)
        {
            anim.Play("Base Layer.Attacking.AttackingS");
        }
        
        lastEventTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        UpdateMovementState();
        if(movementState != MovementState.Idle)
        {
            movingAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            UpdateDirectionState();
        }
    }
}
