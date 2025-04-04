using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    [SerializeField] Player target = null;

    [SerializeField] bool showDetectionRange = true;
    [SerializeField,Range(5.0f,10.0f)] float detectionRange = 0.0f;

    public Player Target => target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindFirstObjectByType<Player>();
    }

    public bool IsClose() => Vector3.Distance(transform.position, target.transform.position) <= detectionRange;

    private void OnDrawGizmos()
    {
        if (!showDetectionRange) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position,detectionRange);

        Gizmos.color = Color.white;
    }
}
