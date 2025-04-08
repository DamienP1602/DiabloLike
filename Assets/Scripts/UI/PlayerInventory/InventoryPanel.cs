using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public event Action<int,ItemStored,ItemStored> OnEquipConsumable;
    public event Action<ItemStored,ItemStored> OnEquipEquipment;


    [Header("Equipments")]
    [SerializeField] EquipmentSlot weaponSlot;

    [Header("Consumables")]
    [SerializeField] EquipmentSlot consumableOneSlot;
    [SerializeField] EquipmentSlot consumableTwoSlot;

    [SerializeField] List<EquipmentSlot> equipSlots;

    [SerializeField] List<ItemSlot> allSlots;

    Inventory playerInventory;
    
    public InventoryItem currentSelectedItem;

    private void Awake()
    {
        allSlots = GetComponentsInChildren<ItemSlot>(true).ToList();
        foreach (ItemSlot _slot in allSlots)
        {
            _slot.OnItemClick += SelectItem;
            _slot.OnItemExecute += ExecuteItem;
        }

        equipSlots = GetComponentsInChildren<EquipmentSlot>(true).ToList();
        foreach (EquipmentSlot _slot in equipSlots)
        {
            _slot.OnItemClick += InteractWithItem;
            _slot.OnItemClick += (_slot) => InitInventoryPanel();
            _slot.ClearItem();
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

    public EquipmentSlot GetEquipmentSlotFromData(ItemStored _data)
    {
        foreach (EquipmentSlot _slot in equipSlots)
        {
            if (_slot.ItemData == _data)
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
        int _slotCapacity = allSlots.Count;

        for (int _i = 0; _i < _slotCapacity; _i++)
        {
            ItemSlot _slot = allSlots[_i];
            if (_slot)
            {
                if (_inventoryCapacity > _i)
                {
                    ItemStored _item = _items[_i];
                    if (_item != null)
                    {
                        _slot.SetItem(_item);
                        continue;
                    }
                }
            }
            _slot.SetSelectionStatus(false);
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
        if (_selectedItem == null) return;

        if (currentSelectedItem)
        {
            currentSelectedItem.ReturnToStartingPos();
            currentSelectedItem.SetSelectionStatus(true);
        }

        currentSelectedItem = _selectedItem;
        currentSelectedItem.SetSelectionStatus(false);
    }

    public void ExecuteItem(InventoryItem _selectedItem)
    {
        if (_selectedItem == null) return;

        _selectedItem.ItemData.item.Execute();
        playerInventory.RemoveItem(_selectedItem.ItemData.item);
        InitInventoryPanel();
    }

    public void InteractWithItem(InventoryItem _slot)
    {
        // Verification if you're clicking on a EquipmentSlot => should be everytime
        EquipmentSlot _equipmentSlot = _slot as EquipmentSlot;
        if (!_equipmentSlot) return;

        // Get a potential item => if there's one, inventory component will switch them
        ItemStored _potentialItemStored = _equipmentSlot.ItemData;

        // No item is selected => try to select it
        if (!currentSelectedItem)
        {
            //If there's no item stored in the current slot => fail try and return
            if (_slot.ItemData == null) return;

            SelectItem(_slot);
            _equipmentSlot.SetSelectionStatus(true);
            _equipmentSlot.ClearItem();

            InvokeItemEventOnType(_equipmentSlot, _potentialItemStored);
            return;
        }

        // If you want to equip an item with the wrong type for the slot => return 
        if (!_equipmentSlot.SetItem(currentSelectedItem.ItemData)) return;

        currentSelectedItem.ClearItem();
        currentSelectedItem = null;

        InvokeItemEventOnType(_equipmentSlot,_potentialItemStored);
    }

    void InvokeItemEventOnType(EquipmentSlot _equipmentSlot, ItemStored _potentialItemStored)
    {
        if (_equipmentSlot.Type == ItemType.CONSOMMABLE || _equipmentSlot.Type == ItemType.CONSOMMABLE)
            OnEquipConsumable?.Invoke((int)_equipmentSlot.Type, _equipmentSlot.ItemData, _potentialItemStored);

        if (_equipmentSlot.Type == ItemType.WEAPON)
            OnEquipEquipment?.Invoke(_equipmentSlot.ItemData, _potentialItemStored);
    }

}
