using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(Light))]
public abstract class Interactable : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] bool showRange;

    [Header("Component")]
    [SerializeField] Light interactLight;
    [SerializeField] bool uniqueInteraction;
    [SerializeField] float cooldown;
    [SerializeField] float range;
    float currentCooldown;
    protected bool isOnCooldown;


    public float Range => range;


    public virtual void Interaction(Player _player)
    {
        interactLight.enabled = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        interactLight = GetComponent<Light>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isOnCooldown)
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= cooldown)
            {
                currentCooldown = 0.0f;
                isOnCooldown = false;
                interactLight.enabled = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position,range);
            Gizmos.color = Color.white;
        }
    }
}
