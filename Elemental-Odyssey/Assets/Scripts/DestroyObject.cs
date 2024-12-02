using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player.SetActive(true);
        Destroy(this.gameObject);
    }
}
