using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Camera Camera => _camera;
    public float DefaultFov => _defaultFov;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _transformOffset;

    [Header("Settings")]
    [SerializeField] private float _defaultFov;

    public void Update()
    {
        //OffsetCamera();
    }

    public void SetFov(float value)
    {
        _camera.fieldOfView = value;
    }

    private void OffsetCamera()
    {
        _camera.transform.position += _transformOffset.localPosition;
        _camera.transform.localRotation = Quaternion.Euler(_camera.transform.localRotation.eulerAngles + _transformOffset.localRotation.eulerAngles);
    }
}
