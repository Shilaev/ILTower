using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObjects : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;

    private GameObject _spawnObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 _touchPosition;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        touchPosition = default;

        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (_spawnObject == null)
            {
                _spawnObject = Instantiate(_objectToSpawn, hitPose.position, hitPose.rotation);
            }
            else
            {
                _spawnObject.transform.position = hitPose.position;
            }
        }
    }
}