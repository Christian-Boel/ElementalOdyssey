using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothTime = .15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0,0, -10));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
