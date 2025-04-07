using System;
using UnityEngine;

public class ClickComponent : MonoBehaviour
{
    public event Action<GameObject> OnTarget = null;
    public event Action<Vector3> OnGround = null;

    bool canClick = true;

    [SerializeField] LayerMask playerLayer = 0;
    [SerializeField] LayerMask groundLayer = 0;
    [SerializeField] LayerMask enemyLayer = 0;

    CameraComponent cameraRef;

    public void Start()
    {
        cameraRef = GetComponent<CameraComponent>();
    }

    public void RaycastOnClick()
    {
        if (canClick)
        {
            Ray _ray = cameraRef.RenderCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _result;

            //If the Ray touch the player, return
            if (Physics.Raycast(_ray, out _result, 20.0f, playerLayer))
                return;

            //If the Ray touch a target, activate OnTarget
            if (Physics.Raycast(_ray, out _result, 100.0f, enemyLayer))
            {
                OnTarget?.Invoke(_result.transform.gameObject);
                return;
            }

            //If the Ray touch the ground, activate OnGround
            if (Physics.Raycast(_ray, out _result, 100.0f, groundLayer))
            {
                OnGround?.Invoke(_result.point);
                return;
            }
        }
    }

    public void SetCanClick(bool _canClick) => canClick = _canClick;
}
