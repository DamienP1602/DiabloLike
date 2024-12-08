using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public InputComponent inputs = null;
    [SerializeField] float moveSpeed = 5.0f, rotateSpeed = 50.0f;

    public List<Flag> path = null;
    int currentIndex = 0;


    void Start()
    {

    }


    void Update()
    {
        if (path != null)
        {
            MoveTo();
            RotateTo();
            CheckDestination();
        }
        else
        {
            Move();
            Rotate();
        }
    }

    void Move()
    {
        float _value = inputs.Forward.ReadValue<float>();

        if (_value == 0.0f) return;
        Vector3 _newLoc = transform.position + transform.forward * _value * moveSpeed * Time.deltaTime;
        transform.position = _newLoc;
    }

    void MoveTo()
    {       
        Vector3 _direction = Vector3.MoveTowards(transform.position, path[currentIndex].position, moveSpeed * Time.deltaTime);
        transform.position = _direction;
    }

    void CheckDestination()
    {
        bool _isNear = Vector3.Distance(transform.position, path[currentIndex].position) <= 0.1f;

        if (_isNear)
        {
            currentIndex++;

            if (currentIndex == path.Count)
            {
                path = null;
                currentIndex = 0;
            }
        }
    }

    void RotateTo()
    {
        Quaternion _newRot = Quaternion.RotateTowards(transform.rotation, path[currentIndex].rotation, rotateSpeed * Time.deltaTime);
        if (_newRot == Quaternion.identity) return;
        transform.rotation = _newRot;
    }

    void Rotate()
    {
        float _value = inputs.Rotate.ReadValue<float>();
        if (_value == 0.0f) return;
        Quaternion _newRotate = Quaternion.Euler(0.0f, _value * rotateSpeed * Time.deltaTime, 0.0f) * transform.rotation;

        transform.rotation = _newRotate;
    }

    public void SetPath(List<Flag> _path)
    {
        if (_path.Count <= 0) return;

        path = _path;
        transform.position = path[currentIndex].position;
        transform.rotation = path[currentIndex].rotation;
    }
}
