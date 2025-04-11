using System;
using UnityEngine;

public class ClickComponent : MonoBehaviour
{
    public event Action<GameObject> OnTarget;
    public event Action<Vector3> OnGround;
    public event Action<Interactable> OnGPE;

    bool overlayMode;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask GPELayer;

    CameraComponent cameraRef;

    public LayerMask EnemyLayer => enemyLayer;
    public LayerMask GroundLayer => groundLayer;

    public void Start()
    {
        cameraRef = GetComponent<CameraComponent>();
    }

    public void RaycastOnClick()
    {
        if (!overlayMode)
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

            //If the Ray touch a GPE, activate OnGPE
            if (Physics.Raycast(_ray, out _result, 100.0f, GPELayer))
            {
                Interactable _interactable = _result.collider.GetComponent<Interactable>();
                OnGPE?.Invoke(_interactable);
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

    public void SetOverlayMode(bool _value) => overlayMode = _value;
}
