using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EquipmentType
{
    NONE,
    CONSUMABLE_ONE,
    CONSUMABLE_TWO,
    WEAPON_RIGHT,
}

public class EquipmentSlot : InventoryItem
{
    [SerializeField] EquipmentType type;

    public EquipmentType Type => type;

    protected override void Awake()
    {
        base.Awake();
    }

    public bool IsItemGoodType(ItemType _type)
    {
        switch (_type)
        {
            case ItemType.NONE:
                return false;
            case ItemType.CONSOMMABLE:
                return type == EquipmentType.CONSUMABLE_ONE || type == EquipmentType.CONSUMABLE_TWO;
            case ItemType.WEAPON:
                return type == EquipmentType.WEAPON_RIGHT;
            case ItemType.ARMOR:
                return false;
        }
        return false;
    }
}
