using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    [SerializeField] CardHandler card = null;

    public void SetCard(CardHandler _card) => card = _card;
}
