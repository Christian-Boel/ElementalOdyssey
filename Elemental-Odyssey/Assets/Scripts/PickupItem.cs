using UnityEngine;

public class PickupItem : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Item"))
        {
            return;
        }

        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            GameManager.Instance.HandlePickup(item);
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.LogError("Picked up an item without an Item component.");
        }
    }
}