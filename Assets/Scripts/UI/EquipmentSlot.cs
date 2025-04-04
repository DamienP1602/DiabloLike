using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : InventoryItem
{
    [SerializeField] ItemType type;

    public ItemType Type => type;

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool SetItem(ItemStored _data)
    {
        if (_data.item.type != type)
            return false;

        return base.SetItem(_data);
    }
}
