using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablePanel : MonoBehaviour
{
    [SerializeField] ConsumableSlot consumableOne;
    [SerializeField] ConsumableSlot consumableTwo;
    
    public ConsumableSlot SlotOne => consumableOne;
    public ConsumableSlot SlotTwo => consumableTwo;

    public ConsumableSlot GetSlotFromData(ItemStored _data)
    {
        //S'il y a quelque chose dans le premier slot
        if (consumableOne.Item == _data)
            return consumableOne;

        //S'il y a quelque chose dans le deuxieme slot
        else if (consumableTwo.Item == _data)
            return consumableTwo;

        //S'il n'y a rien
        else if (consumableTwo.Item.item == null && consumableOne.Item.item == null)
            return consumableOne;

        //S'il y a quelque chose dans le premier slot et le deuxieme slot
        else
            return consumableTwo;
    }

    public ConsumableSlot GetFromIndex(int _index) => _index == 1 ? consumableOne : _index == 2 ? consumableTwo : null;

    public ConsumableSlot GetFirstEmptySlot()
    {
        if (consumableOne.Item.item == null)
            return consumableOne;
        else
            return consumableTwo;
    }
}
