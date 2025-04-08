using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ItemStored
{
    public BaseItem item;
    public int amount;

    public ItemStored(BaseItem _item)
    {
        item = _item;
        amount = 1;
    }
}

public class Inventory : MonoBehaviour
{
    public event Action<ItemStored> OnConsumableUse = null;

    int inventorySize = 32;
    [SerializeField] List<ItemStored> allItems = new();

    [SerializeField] ItemStored consumableSlotOne = null;
    [SerializeField] ItemStored consumableSlotTwo = null;

    int gold = 0;

    public int Gold => gold;
    public List<ItemStored> AllItems => allItems;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(BaseItem _item, int _amout = 1)
    {
        if (allItems.Count >= inventorySize)
            return;

        // if Contains
        if (_item.canBeStacked)
        {
            foreach (ItemStored _itemStored in allItems)
            {
                if (_item == _itemStored.item)
                {
                    _itemStored.amount += _amout;
                    return;
                }
            }
        }


        // if not Contains
        _item.InitOwner(GetComponent<Player>());
        ItemStored _newItem = new ItemStored(_item);

        allItems.Add(_newItem);
        _newItem.amount = _amout;
    }

    public void RemoveItem(BaseItem _item)
    {
        foreach (ItemStored _itemStored in allItems)
        {
            if (_item == _itemStored.item)
            {
                _itemStored.amount--;

                if (_itemStored.amount == 0)
                {
                    // Reset for SO data
                    _itemStored.item.owner = null;

                    allItems.Remove(_itemStored);
                    return;
                }
            }
        }
    }

    void RemoveConsumable(ItemStored _consumable)
    {
        _consumable.amount--;

        if (_consumable.amount == 0)
        {
            // Reset for SO data
            _consumable.item.owner = null;
            _consumable.item = null;
        }
    }

    public void DestroyItem(BaseItem _item, bool _resetOwner)
    {
        foreach (ItemStored _itemStored in allItems)
        {
            if (_item == _itemStored.item)
            {
                // Reset for SO data
                if (_resetOwner)
                    _itemStored.item.owner = null;

                allItems.Remove(_itemStored);
                return;
            }
        }
    }

    public void SetConsumable(EquipmentType _type, ItemStored _consumable, ItemStored _oldData = null)
    {
        if (_type == EquipmentType.CONSUMABLE_ONE)
        {
            if (consumableSlotOne.item == null)
                consumableSlotOne = _consumable;
            else
            {
                consumableSlotOne = _consumable;
                AddItem(_oldData.item, _oldData.amount);
            }

        }
        else if (_type == EquipmentType.CONSUMABLE_TWO)
        {
            if (consumableSlotTwo.item == null)
                consumableSlotTwo = _consumable;
            else
            {
                consumableSlotTwo = _consumable;
                AddItem(_oldData.item, _oldData.amount);
            }
        }
        DestroyItem(_consumable.item, false);
    }

    public void RemoveConsumable(EquipmentType _type, ItemStored _currentConsumable)
    {
        AddItem(_currentConsumable.item, _currentConsumable.amount);

        if (_type == EquipmentType.CONSUMABLE_ONE)
            consumableSlotOne.item = null;

        if (_type == EquipmentType.CONSUMABLE_TWO)
            consumableSlotTwo.item = null;
    }

    public void UseConsumable(InputAction.CallbackContext _context)
    {
        float _consumableIndex = _context.ReadValue<float>();

        bool _hasSelectedConsumableEquiped = _consumableIndex > 0 ? consumableSlotOne != null : consumableSlotTwo != null;

        ItemStored _itemToUse = _consumableIndex > 0 ? consumableSlotOne : consumableSlotTwo;

        if (!_hasSelectedConsumableEquiped || !_itemToUse.item)
            return;

        _itemToUse.item.Execute();
        RemoveConsumable(_itemToUse);

        OnConsumableUse?.Invoke(_itemToUse);
    }

    public void EquipItem(EquipmentType _type, ItemStored _data, ItemStored _oldData = null)
    {
        StatsComponent _ownerStats = GetComponent<StatsComponent>();
        if (!_ownerStats) return;

        SO_Weapon _weapon = _data.item as SO_Weapon;
        if (!_weapon) return;

        if (_oldData != null)
        {
            SO_Weapon _oldWeapon = _oldData.item as SO_Weapon;
            if (!_oldWeapon) return;

            _oldWeapon.Unequip(_ownerStats);
            AddItem(_oldWeapon);
        }

        _weapon.Equip(_ownerStats);
        DestroyItem(_data.item, false);
    }

    public void DesequipItem(EquipmentType _type, ItemStored _currentItem)
    {
        StatsComponent _ownerStats = GetComponent<StatsComponent>();
        if (!_ownerStats) return;

        SO_Weapon _oldWeapon = _currentItem.item as SO_Weapon;
        if (!_oldWeapon) return;

        _oldWeapon.Unequip(_ownerStats);
        AddItem(_oldWeapon);

    }
}
