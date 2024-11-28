using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] Camera self = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        ClickUpdate();
    }

    void Init()
    {
        self = GetComponent<Camera>();
    }

    void ClickUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Raycast(out RaycastHit _raycast))
            {
                RaycastTrigger(_raycast);
            }
        }
    }
    bool Raycast(out RaycastHit _hit)
    {
        Ray _ray = self.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(_ray, out _hit);
    }

    void RaycastTrigger(RaycastHit _raycast)
    {
        CustomButton _button = _raycast.collider.gameObject.GetComponent<CustomButton>();
        if (_button)
            _button.Click();
    }
}
