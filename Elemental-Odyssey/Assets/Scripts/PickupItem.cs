using TMPro;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool pickedUp = false;
    public GameObject pickUpCanvas;
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
        if (item != null && !pickedUp)
        {
            pickedUp = true;
            GameManager.Instance.HandlePickup(item);
            SoundFXManager.instance.PlaySoundFXClip(item.ItemSoundClip, transform, 0.5f);
            Destroy(collision.gameObject);
            if(item.itemType == ItemType.Upgrade)
            {
                GameObject pickup = Instantiate(pickUpCanvas, transform.position, Quaternion.identity);
                pickup.transform.Find("PowerUp Text").GetComponent<TextMeshProUGUI>().text = "+" + item.upgrade.value + " " + item.upgrade.upgradeType;
            }
        }
        else
        {
            Debug.LogError("Picked up an item without an Item component.");
        }
        pickedUp = false;
    }
}