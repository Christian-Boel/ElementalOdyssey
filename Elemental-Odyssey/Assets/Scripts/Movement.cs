using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Movement : MonoBehaviour, IStats
{
    [SerializeField] private float MSSpeed = 5f;            // Normal movement speed
    [SerializeField] private float dashSpeed = 18f;         // Speed during dash
    [SerializeField] private float dashDuration = 0.1f;     // Duration of dash in seconds
    [SerializeField] private float dashCooldown = 4f;     // Cooldown time between dashes
    [SerializeField] private AudioClip[] dashSoundClips;
    
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;
    private Vector2 lastMoveDirection;

    public bool attacking;
    public float attackPenalty = .3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateStats();
    }

    void Update()
    {
        // Detect spacebar press for dashing
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
            SoundFXManager.instance.PlayRandomSoundFXClip(dashSoundClips, transform, .5f);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Apply dash velocity
            rb.velocity = dashDirection * dashSpeed;
            return; // Skip normal movement while dashing
        }

        // Normal movement code
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.magnitude > 1)
        {
            input = input.normalized;
        }

        float attackSlowdown = attacking ? attackPenalty : 1f;

        rb.velocity = MSSpeed * attackSlowdown * input;

        // Store last move direction
        if (input != Vector2.zero)
        {
            lastMoveDirection = input.normalized;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // Determine dash direction
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (dashDirection == Vector2.zero)
        {
            dashDirection = lastMoveDirection != Vector2.zero ? lastMoveDirection : Vector2.right;
        }
        else
        {
            dashDirection = dashDirection.normalized;
        }

        // Wait for the updated dash duration
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Wait for the current dash cooldown
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    public void UpdateStats()
    {
        PlayerStats stats = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<PlayerStats>();
        MSSpeed = stats.MS;
        dashCooldown = stats.dashCD;
        dashDuration = stats.dashLength;
    }
}
