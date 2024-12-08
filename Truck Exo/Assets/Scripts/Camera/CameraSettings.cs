using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Offset
{
    [SerializeField] bool isLocal;
    [SerializeField, Header("Movement Offset")] float xOffset;
    [SerializeField] float yOffset, zOffset;
    [SerializeField, Header("View Offset")] float xViewOffset;
    [SerializeField] float yViewOffset, zViewOffset;

    public Vector3 GetOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;

        return isLocal ? GetLocalOffset(_target) : GetWorldOffset(_target);
    }

    Vector3 GetLocalOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;

        Vector3 _x = _target.right * xOffset;
        Vector3 _y = _target.up * yOffset;
        Vector3 _z = _target.forward * zOffset;
        return _target.position + _x + _y + _z;
    }

    Vector3 GetWorldOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        return _target.position + new Vector3(xOffset, yOffset, zOffset);
    }

    public Vector3 GetViewOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        return isLocal ? GetLocalViewOffset(_target) : GetWorldViewOffset(_target);
    }

    public Vector3 GetWorldViewOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;
        return _target.position + new Vector3(xViewOffset, yViewOffset, zViewOffset);
    }

    public Vector3 GetLocalViewOffset(Transform _target)
    {
        if (!_target) return Vector3.zero;

        Vector3 _x = _target.right * xViewOffset;
        Vector3 _y = _target.up * yViewOffset;
        Vector3 _z = _target.forward * zViewOffset;
        return _target.position + _x + _y + _z;
    }
}




[Serializable]
public class CameraSettings
{
    [SerializeField] Transform target = null;
    [SerializeField] Camera cameraRender = null;
    [SerializeField] float moveSpeed = 5.0f, rotationSpeed = 5.0f;
    [SerializeField] bool canMove = true, canRotate = true;
    [SerializeField] Offset offset = new Offset();

    public bool IsValid => target && cameraRender;
    public Transform Target => target;
    public Camera CameraRender => cameraRender;
    public Vector3 TargetPos => IsValid ? target.position : Vector3.zero;
    public Vector3 CurrentPos => IsValid ? cameraRender.transform.position : Vector3.zero;
    public Quaternion TargetRot => IsValid ? target.rotation : Quaternion.identity;
    public Quaternion CurrentRot => IsValid ? cameraRender.transform.rotation : Quaternion.identity;
    public float MoveSpeed => moveSpeed;
    public float RotationSpeed => rotationSpeed;
    public bool CanRotate => canRotate;
    public bool CanMove => canMove;
    public Offset Offset => offset;

    public void SetCanMove(bool _value) => canMove = _value;
    public void SetCanRotate(bool _value) => canRotate = _value;
    public void SetEnable(bool _value) => cameraRender.enabled = _value;
    public void InitCameraRender(Transform _origin)
    {
        if (!_origin) return;

        cameraRender = _origin.GetComponent<Camera>();
    }

    public void SetTarget(Transform _target)
    {
        if (!_target) return;

        target = _target;
    }

    public void SetCameraRender(Camera _camera)
    {
        if (!_camera) return;

        cameraRender = _camera;
    }
}
