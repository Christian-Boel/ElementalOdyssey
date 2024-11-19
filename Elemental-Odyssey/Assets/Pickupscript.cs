using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickupscript : MonoBehaviour
{
    public GameManager GameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Key") || collision.CompareTag("Potion")) GameManager.handlepickup(collision.name);
        Destroy(collision.gameObject);
    }
}
