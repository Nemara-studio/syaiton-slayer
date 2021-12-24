using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [Range(1, 10)]
    [SerializeField] private float smoothFactor;

    private void FixedUpdate()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
