﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    float _rotateSpeed = 1.0f;

    [Header("Zoom")]
    [SerializeField]
    float _zoomSpeed = 1.0f;
    [SerializeField]
    float minZoom = 30f;
    [SerializeField]
    float maxZoom = 100f;

    public void RotateAroundTransform(Transform targetTransform, float xAxis)
    {
        transform.RotateAround(targetTransform.position, targetTransform.up, xAxis * _rotateSpeed);
    }

    public void ZoomIn(Vector3 targetPosition)
    {
        if (transform.position.y > minZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _zoomSpeed);
        }
    }

    public void ZoomOut(Vector3 targetPosition)
    {
        if (transform.position.y < maxZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, -_zoomSpeed);
        }
    }
}