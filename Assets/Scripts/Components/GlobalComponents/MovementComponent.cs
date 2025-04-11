using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MovementComponent : NetworkBehaviour
{
    AnimationComponent animRef;

    [Header("DEBUG")]
    [SerializeField] bool drawDestination;

    [Header("Component")]
    [SerializeField] float moveSpeed, rotateSpeed;
    [SerializeField] float minDist;
    [SerializeField] bool canMove;

    public float cantMoveDuration;
    [SerializeField] float cantMoveProgession;

    [SerializeField] Vector3 destination;
    [SerializeField] GameObject target ;

    [SerializeField] Vector3 rotationTarget;
    [SerializeField] bool needToRotate;

    public Vector3 Destination => destination;
    public GameObject Target => target;

    public bool IsAtLocation()
    {
        return Vector3.Distance(destination, transform.position) <= minDist;
    }

    public bool IsAtRotationTarget()
    {
        return (rotationTarget - transform.position) == Vector3.zero;
    }

    void Start()
    {
        destination = transform.position;
        animRef = GetComponent<AnimationComponent>();
    }

    void Update()
    {
        if (target)
            destination = SetDestinationFromTarget();

        RotateToDestination();

        if (!canMove)
        {
            OnMoveRestriction();
            return;
        }
        MoveToDestination();
    }

    Vector3 SetDestinationFromTarget()
    {
        Vector3 _targetPos = target.transform.position;
        return new Vector3(_targetPos.x,transform.position.y,_targetPos.z);
    }

    public void SetRotationTarget(bool _value,Vector3 _target = default)
    {
        rotationTarget = _target;
        needToRotate = _value;
    }

    public void SetCantMove(float _duration)
    {
        canMove = false;
        cantMoveDuration = _duration;

        animRef.StopMovementAnimation();
    }

    void OnMoveRestriction()
    {
        if (cantMoveDuration >= 0.0f)
        {
            cantMoveProgession += Time.deltaTime;
            if (cantMoveProgession >= cantMoveDuration)
            {
                cantMoveDuration = 0.0f;
                cantMoveProgession = 0.0f;
                canMove = true;
            }
        }
    }

    void MoveToDestination()
    {
        if (IsAtLocation()) return;

        animRef.StartMovementAnimation();

        Vector3 _newPosition = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.position = _newPosition;

        if (IsAtLocation())
        {
            animRef.StopMovementAnimation();
            return;
        }
    }

    void RotateToDestination()
    {
        if (needToRotate)
        {
            Rotate(rotationTarget);
        }
        else
        {
            Rotate(Destination);
        }
    }

    void Rotate(Vector3 _destination)
    {
        Vector3 _lookAt = _destination - transform.position;
        if (_lookAt == Vector3.zero) return;

        Quaternion _rot = Quaternion.LookRotation(_lookAt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotateSpeed);
    }

    public void SetDestination(Vector3 _destination)
    {
        destination = _destination;
        minDist = 0.0f;
    }

    public void SetTarget(GameObject _target, float _minimumRange)
    {
        target = _target;
        minDist = _minimumRange;
    }

    private void OnDrawGizmos()
    {
        if (drawDestination)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Destination);
            Gizmos.DrawWireSphere(Destination, 1.0f);
        }
    }
}
