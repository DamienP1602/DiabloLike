using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraComponent : NetworkBehaviour
{
    [SerializeField] Camera cameraRef;
    [SerializeField] Camera renderCamera;
    [SerializeField] Vector3 offset;
    [SerializeField] float moveSpeed;

    public Transform targetToFollow;
    public Camera RenderCamera => renderCamera;

    void Start()
    {

    }

    public void CreateCamera()
    {
        renderCamera = Instantiate(cameraRef, gameObject.transform.position, Quaternion.identity);
        renderCamera.transform.eulerAngles = new Vector3(40.0f, -45.0f, 0.0f);

        offset = new Vector3(5.0f, 5.0f, -5.0f);
        targetToFollow = gameObject.transform;
        DontDestroyOnLoad(renderCamera);
    }

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
