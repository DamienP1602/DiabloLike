using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public event Action<int,ItemStored> OnEquipConsumable;
    public event Action<ItemStored,ItemStored> OnEquipEquipment;

    [SerializeField] List<ItemSlot> allSlots;
    [SerializeField] List<EquipmentSlot> equipSlots;

    Inventory playerInventory;

    public InventoryItem currentSelectedItem;

    private void Awake()
    {
        allSlots = GetComponentsInChildren<ItemSlot>(true).ToList();
        foreach (ItemSlot _slot in allSlots)
        {
            _slot.OnItemClick += SelectItem;
        }

        equipSlots = GetComponentsInChildren<EquipmentSlot>(true).ToList();
        foreach (EquipmentSlot _slot in equipSlots)
        {
            _slot.OnItemClick += EquipItem;
            _slot.OnItemClick += (_item) => InitInventoryPanel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentSelectedItem)
            currentSelectedItem.MoveFromMouse();
    }

    public void SetInventory(Inventory _inventory) => playerInventory = _inventory; 

    public int GetConsumableSlotIndex(ItemStored _item)
    {
        int _index = 1;
        foreach (EquipmentSlot _slot in equipSlots)
        {
            if (_slot.Type == ItemType.CONSOMMABLE)
            {
                if (_slot.Item == _item)
                    return _index;
                _index++;
            }                
        }
        return -1;
    }

    public EquipmentSlot GetEquipmentSlotFromData(ItemStored _data)
    {
        foreach (EquipmentSlot _slot in equipSlots)
        {
            if (_slot.Item == _data)
                return _slot;
        }
        return null;
    }

    public void Open()
    {
        gameObject.SetActive(true);

        InitInventoryPanel();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void InitInventoryPanel()
    {
        ResetInventory();

        List<ItemStored> _items = playerInventory.AllItems;
        int _inventoryCapacity = _items.Count;

        for (int _i = 0; _i < _inventoryCapacity; _i++)
        {
            ItemSlot _slot = allSlots[_i];
            if (!_slot) continue;

            ItemStored _item = _items[_i];
            if (_item == null) continue;

            _slot.SetItem(_item); 
        }
    }

    public void ResetInventory()
    {
        currentSelectedItem = null;
        foreach (ItemSlot _slot in allSlots)
        {
            _slot.ClearItem();
            _slot.SetSelectionStatus(true);
        }
    }

    public void SelectItem(InventoryItem _selectedItem)
    {
        if (_selectedItem == null)
            return;

        if (currentSelectedItem)
        {
            currentSelectedItem.ReturnToStartingPos();
            currentSelectedItem.SetSelectionStatus(true);
        }

        currentSelectedItem = _selectedItem;
        currentSelectedItem.SetSelectionStatus(false);
    }

    public void EquipItem(InventoryItem _slot)
    {
        // No item is selected
        if (!currentSelectedItem) return;

        // Verification if you're clicking on a EquipmentSlot => should be everytime
        EquipmentSlot _equipmentSlot = _slot as EquipmentSlot;
        if (!_equipmentSlot) return;

        // Get a potential item => if there's one, inventory component will switch them
        ItemStored _potentialItemStored = _equipmentSlot.Item;

        // If you want to equip an item with the wrong type for the slot => return 
        if (!_equipmentSlot.SetItem(currentSelectedItem.Item)) return;

        currentSelectedItem.ClearItem();
        currentSelectedItem = null;

        switch (_equipmentSlot.Type)
        {
            case ItemType.CONSOMMABLE:
                OnEquipConsumable?.Invoke(GetConsumableSlotIndex(_slot.Item), _slot.Item);
                break;
            case ItemType.WEAPON:
                OnEquipEquipment?.Invoke(_slot.Item, _potentialItemStored);
                break;
            case ItemType.ARMOR:

                break;
            default:
                break;
        }
    }
}
