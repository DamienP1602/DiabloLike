using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraTPS : CameraBehaviour
{
    public override void CameraMoveTo()
    {
        if (!settings.CanMove) return;

        Vector3 _offset = settings.Offset.GetOffset(settings.Target);
        transform.position = Vector3.MoveTowards(transform.position, _offset, Time.deltaTime * settings.MoveSpeed);
    }

    public override void CameraRotateTo()
    {
        if (!settings.CanRotate) return;

        //Vector3 _lookAt = settings.TargetPos - transform.position; //Cible la target directement
        Vector3 _lookAt = settings.Offset.GetViewOffset(settings.Target) - transform.position;
        if (_lookAt == Vector3.zero) return;

        Quaternion _rot = Quaternion.LookRotation(_lookAt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * settings.RotationSpeed);
    }

}
