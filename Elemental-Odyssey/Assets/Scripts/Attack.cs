using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Reference to the script containing the method to call
    private PlayerAnimations animator;
    private Movement playerMovement;
    private float aaSpeed = 0.767f;
    public float aaCD = 1;
    private bool attackReady = true;
    public GameObject attackObject;


    private void Start()
    {
        aaCD += aaSpeed;
        animator = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<Movement>();
    }
    void Update()
    {
        // Check if the left mouse button (M1) was clicked
        if (Input.GetMouseButtonDown(0) && attackReady)
        {
            attackReady = false;
            playerMovement.attacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        // Get the player's position
        Vector2 playerPosition = transform.position;

        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction vector from the player to the mouse
        Vector2 direction = mousePosition - playerPosition;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Call the method on the other script, passing the angle
        animator.UpdateAttackState(angle);

        yield return new WaitForSeconds(aaSpeed);
        playerMovement.attacking = false;
        angle = Mathf.Round(angle/90)*90;
        Instantiate(attackObject, transform.position + new Vector3(0, 0.4f, 0), Quaternion.Euler(0, 0, angle));

        yield return new WaitForSeconds(aaCD);
        attackReady = true;
    }
}
