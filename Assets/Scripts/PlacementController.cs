using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    // [SerializeField] private GameObject _placedPrefab;
    private ARRaycastManager _arRaycastManager;
    private static List<ARRaycastHit> _aRRaycastHits = new List<ARRaycastHit>();


    // public GameObject PlacedPrefab
    // {
    //     get => _placedPrefab;
    //     set => _placedPrefab = value;
    // }

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (_arRaycastManager.Raycast(touchPosition, _aRRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = _aRRaycastHits[0].pose;

            Instantiate(_arRaycastManager.raycastPrefab, hitPos.position, hitPos.rotation);
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        touchPosition = default;

        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        return false;
    }
}