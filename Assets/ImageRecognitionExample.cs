using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionExample : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    private GameObject _spawnObject;
    private ARTrackedImageManager _arTrackedImageManager;

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        if (_spawnObject == null)
        {
            _spawnObject = Instantiate(_object);
            _spawnObject.transform.position = _arTrackedImageManager.trackedImagePrefab.transform.position;
        }

    }


}
