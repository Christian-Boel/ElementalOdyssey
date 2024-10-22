using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;
    public float smoothSpeed = .1f;

    private void Start()
    {
    }
    void Update()
    {
        playerPosition = player.GetComponent<Transform>().position;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, playerPosition, smoothSpeed);

        smoothedPosition.z = -10f;

        transform.position = smoothedPosition;
    }
}
