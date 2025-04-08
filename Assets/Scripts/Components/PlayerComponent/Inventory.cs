using System;
using System.Collections.Generic;
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
    
    public void AddItem(BaseItem _item)
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
                    _itemStored.amount++;
                    return;
                }
            }
        }

        // if not Contains
        _item.InitOwner(GetComponent<Player>());
        allItems.Add(new ItemStored(_item));
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

    // TODO => Pas ouf à refaire
    public void SetConsumable(int _index,ItemStored _consumable, ItemStored _oldData = null)
    {
        if (_consumable == null)
        {
            AddItem(_oldData.item);

            if (_index == 1)
                consumableSlotOne.item = null;
            if (_index == 2)
                consumableSlotTwo.item = null;

            return;
        }

        if (_index == 1)
        {
            if (consumableSlotOne.item == null)
            {
                consumableSlotOne = _consumable;
            }
            else
            {
                BaseItem _temp = _consumable.item;
                consumableSlotOne = _consumable;
                AddItem(_temp);
            }
            DestroyItem(_consumable.item, false);
        }
        else if (_index == 2)
        {
            if (consumableSlotTwo.item == null)
            {
                consumableSlotTwo = _consumable;
            }
            else
            {
                BaseItem _temp = _consumable.item;
                consumableSlotTwo = _consumable;
                AddItem(_temp);
            }
            DestroyItem(_consumable.item, false);
        }
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

    public void EquipItem(ItemStored _data, ItemStored _oldData = null)
    {
        StatsComponent _ownerStats = GetComponent<StatsComponent>();
        if (!_ownerStats) return;

        // If there's data => equip item
        if (_data != null)
        {
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
        // _data = null => remove old item for select item in UI
        else
        {
            SO_Weapon _oldWeapon = _oldData.item as SO_Weapon;
            if (!_oldWeapon) return;

            _oldWeapon.Unequip(_ownerStats);
            AddItem(_oldWeapon);
        }        
    }
}
