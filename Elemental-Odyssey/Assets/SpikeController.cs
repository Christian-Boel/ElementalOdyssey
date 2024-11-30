using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public bool active = false;
    private bool isActive = false;


    private void Update()
    {
        if (active && !isActive)
        {
            isActive = true;
            ActivateSpikes(true);
        }
        else if(!active && isActive)
        {
            isActive = false;
            ActivateSpikes(false);
        }
    }
    public void ActivateSpikes(bool activate)
    {
        Transform spikeController = transform;
        foreach (Transform spike in spikeController)
        {
            spike.gameObject.GetComponent<Spike>().active = activate;
        }
    }
}
