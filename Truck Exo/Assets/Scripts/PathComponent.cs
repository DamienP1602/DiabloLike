using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Flag
{
    public Vector3 position {  get; set; }
    public Quaternion rotation { get; set; }
}


public class PathComponent : MonoBehaviour
{
    public MoveComponent moveComponent = null;
    [SerializeField] List<Flag> path = new List<Flag>();
    [SerializeField] bool registerPath = false;
    [SerializeField] float currentTime = 0.0f, maxTime = 0.5f;

    public List<Flag> Path => path;
    public bool IsRegisteringPath => registerPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (registerPath)
            UpdateTimer();
    }

    void UpdateTimer()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            CreatePath();
            currentTime = 0.0f;
        }
    }

    void CreatePath() => path.Add(new Flag() { position = transform.position, rotation = transform.rotation });

    public void ToggleRegister()
    {
        registerPath = !registerPath;

        if (registerPath)
        {
            currentTime = 0.0f;
            path.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        if (path.Count == 0) return;

        Gizmos.color = Color.red;

        int _size = path.Count;
        for (int i = 0; i < _size; i++)
        {
            Gizmos.DrawSphere(path[i].position,0.5f);
        }
    }
}
