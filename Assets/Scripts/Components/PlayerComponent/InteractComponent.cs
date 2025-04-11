using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    [SerializeField] Interactable target;

    public bool IsNearEnough => Vector3.Distance(transform.position, target.transform.position) <= target.Range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target && IsNearEnough)
        {
            target.Interaction(GetComponent<Player>());
            target = null;
        }
    }

    public void SetTarget(Interactable _target) => target = _target;
}
