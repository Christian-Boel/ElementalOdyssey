using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Animator ani;
    private Collider2D collider;
    public bool active = false;
    private bool activating = false;
    public float minTime = 1;
    public float maxTime = 5;
    public float reactionTime = 1;
    public float spikeTime = .5f;
    public float spikeUpTime = .1f;

    public float dmg = 10;

    private bool damaged = false;


    private void Start()
    {
        ani = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(active && !activating)
        {
            activating = true;
            StartCoroutine(SpikeCoroutine());
        }
    }

    IEnumerator SpikeCoroutine()
    {
        float randomFloat = Random.Range(minTime, maxTime);

        yield return new WaitForSeconds(randomFloat);
        ani.SetTrigger("Trigger");

        yield return new WaitForSeconds(reactionTime);
        ani.SetTrigger("Trigger");


        yield return new WaitForSeconds(spikeTime);
        collider.enabled = true;


        yield return new WaitForSeconds(spikeUpTime);
        collider.enabled = false;
        damaged = false;
        activating = false;

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !damaged)
        {
            damaged = true;
            GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<PlayerStats>().TakeDmg(dmg);
        }
    }
}
