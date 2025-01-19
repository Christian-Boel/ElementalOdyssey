using System.Collections;
using TMPro;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool cd = false;
    public GameObject pickUpCanvas;
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Item") || cd)
        {
            return;
        }
        cd = true;
        StartCoroutine(CooldownDelay(.5f));
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
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
        cd = false;
    }

    private IEnumerator CooldownDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        cd = false;
    }
}