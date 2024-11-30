using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    [SerializeField] CardHandler card = null;
    [SerializeField] Vector3 slotPosition = Vector3.zero;

    public bool HasCard => card;

    void Start()
    {
        slotPosition = transform.position;
    }
    public void SetCard(CardHandler _card)
    {
        card = _card;   
        card.transform.position = slotPosition;
    }
}
