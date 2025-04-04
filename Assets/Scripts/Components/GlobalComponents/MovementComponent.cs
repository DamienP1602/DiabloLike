using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    AnimationComponent animRef = null;


    [Header("DEBUG")]
    [SerializeField] bool drawDestination;

    [Header("Component")]
    [SerializeField] float moveSpeed = 10.0f, rotateSpeed = 50.0f;
    [SerializeField] float minDist = 0.0f;
    [SerializeField] bool canMove = true;

    public float cantMoveDuration = 0.0f;
    [SerializeField] float cantMoveProgession = 0.0f;

    [SerializeField] Vector3 destination = Vector3.zero;
    [SerializeField] GameObject target = null;

    public Vector3 Destination => destination;
    public GameObject Target => target;

    public bool IsAtLocation()
    {
        return Vector3.Distance(destination, transform.position) <= minDist;
    }

    void Start()
    {
        destination = transform.position;
        animRef = GetComponent<AnimationComponent>();
    }

    void Update()
    {
        if (target)
            destination = target.transform.position;

        if (!canMove)
        {
            OnMoveRestriction();
            return;
        }


        MoveToDestination();
        RotateToDestination();
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
        if (IsAtLocation())
        {
            animRef.StopMovementAnimation();
            return;
        }

        animRef.StartMovementAnimation();

        Vector3 _newPosition = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        transform.position = _newPosition;
    }

    void RotateToDestination()
    {
        Vector3 _lookAt = Destination - transform.position;
        if (_lookAt == Vector3.zero)
            return;

        Quaternion _rot = Quaternion.LookRotation(_lookAt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotateSpeed);
    }

    public void SetDestination(Vector3 _destination)
    {
        destination = _destination;
        minDist = 0.0f;
    }

    public void SetTarget(GameObject _target, float _rangeAttack)
    {
        target = _target;
        minDist = GetComponent<AttackComponent>().Range;
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
