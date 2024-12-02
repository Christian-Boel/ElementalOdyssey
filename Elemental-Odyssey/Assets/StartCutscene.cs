using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public GameObject endingCutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player")){
            endingCutscene.SetActive(true);
        }

    }
}
