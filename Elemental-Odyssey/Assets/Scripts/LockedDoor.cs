using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public KeyType requiredKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GameManager.Instance.HasKey(requiredKey))
            {
                Debug.Log("Door is locked. You need the " + requiredKey + " to unlock it.");
                return;
            }
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        Debug.Log("Door unlocked!");
        gameObject.SetActive(false);
    }
}