using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 currentVelocity = Vector3.zero;


    private void Awake()
    {
        offset = transform.position - target.position;    
    }

    
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, targetPosition.z), ref currentVelocity, smoothTime);
    }
}
