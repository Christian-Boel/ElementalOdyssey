using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public bool active = false;
    public bool i = false;


    private void Update()
    {
        if (active && !i)
        {

        }   
    }
    public void ActivateSpikes()
    {
        Transform spikeController = transform;
        foreach (Transform spike in spikeController)
        {
            spike.gameObject.GetComponent<Spike>().active = true;
        }
    }
}
