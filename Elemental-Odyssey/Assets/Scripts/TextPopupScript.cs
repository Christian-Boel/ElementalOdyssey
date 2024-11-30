using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopupScript : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) text.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) text.SetActive(false);
    }
}
