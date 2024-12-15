using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public GameObject endingCutscene;
    public GameObject musicManager;
    public GameObject FXManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            collision.gameObject.SetActive(false);
            MusicManager.instance.GetComponent<AudioSource>().Stop();
            FXManager.SetActive(false);
            endingCutscene.SetActive(true);
        }

    }
}
