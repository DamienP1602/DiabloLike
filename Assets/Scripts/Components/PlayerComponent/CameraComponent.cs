using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Camera renderCamera = null;
    [SerializeField] Vector3 offset = new Vector3(5.0f,5.0f,-5.0f);
    [SerializeField] float moveSpeed = 5.0f;

    public Transform targetToFollow = null;
    public Camera RenderCamera => renderCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //
    // Update is called once per frame
    void Update()
    {
        if (!renderCamera) return;

        Move();
    }

    void Move()
    {
        Vector3 _targetPosition = targetToFollow.position;
        
        Vector3 _newPos = Vector3.MoveTowards(renderCamera.transform.position, _targetPosition + offset, Time.deltaTime * moveSpeed);
        renderCamera.transform.position = _newPos;
    }
}
