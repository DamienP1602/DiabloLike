using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] InputComponent inputsComponent = null;
    [SerializeField] MoveComponent moveComponent = null;
    [SerializeField] PathComponent pathComponent = null;

    void Start()
    {
        Init();
    }

    void Init()
    {
        inputsComponent = transform.AddComponent<InputComponent>();
        moveComponent = transform.AddComponent<MoveComponent>();
        pathComponent = transform.AddComponent<PathComponent>();

        moveComponent.inputs = inputsComponent;
        pathComponent.moveComponent = moveComponent;

        inputsComponent.Path.performed += (e) => { pathComponent.ToggleRegister(); };
        inputsComponent.StartAutoPath.performed += (e) => 
        {
            if (pathComponent.IsRegisteringPath) return;

            moveComponent.SetPath(pathComponent.Path);
        };
    }


    void Update()
    {
        
    }
}
