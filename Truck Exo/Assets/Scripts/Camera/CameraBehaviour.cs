using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBehaviour : MonoBehaviour
{
    [SerializeField, Header("Cam Id")] protected string id = "Camera";
    [SerializeField, Header("Cam Settings")] protected CameraSettings settings = new CameraSettings();

    public string ID => id;
    public CameraSettings Settings => settings;
    public bool IsValid => !string.IsNullOrEmpty(id);

    // Start is called before the first frame update
    void Start()
    {
        InitItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void LateUpdate()
    {
        CameraMoveTo();
        CameraRotateTo();
    }

    public abstract void CameraMoveTo();

    public abstract void CameraRotateTo();

    public void InitItem()
    {
        settings.InitCameraRender(transform);
    }

    public void SetSetting(CameraSettings _settings)
    {
        settings = _settings;
    }

    public void Enable()
    {
        settings.SetEnable(true);
    }

    public void Disable()
    {
        settings.SetEnable(false);
    }
}
