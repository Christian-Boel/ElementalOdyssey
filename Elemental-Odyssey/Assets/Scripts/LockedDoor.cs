using TMPro;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public KeyType requiredKey;
    public GameObject textBox;
    private TextMeshProUGUI textComp;


    private void Start()
    {
        textComp = textBox.GetComponent<TextMeshProUGUI>();
        textComp.text = "Sorry you are missing the " + requiredKey;
        textBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GameManager.Instance.HasKey(requiredKey))
            {
                textBox.SetActive(true);
                return;
            }
            UnlockDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }

    private void UnlockDoor()
    {
        Debug.Log("Door unlocked!");
        gameObject.SetActive(false);
    }
}