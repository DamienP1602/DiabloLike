using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    [SerializeField] IIA_Player controls = null;
    [SerializeField] InputAction forward = null;
    [SerializeField] InputAction rotate = null;
    [SerializeField] InputAction path = null;
    [SerializeField] InputAction startAutoPath = null;

    public InputAction Forward => forward;
    public InputAction Rotate => rotate;
    public InputAction Path => path;
    public InputAction StartAutoPath => startAutoPath;


    private void Awake()
    {
        controls = new IIA_Player();
    }

    private void OnEnable()
    {
        forward = controls.Player.Forward;
        rotate = controls.Player.Rotate;
        path = controls.Player.Path;
        startAutoPath = controls.Player.StartAutoPath;

        forward.Enable();
        rotate.Enable();
        path.Enable();
        startAutoPath.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
