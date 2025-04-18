using System.Collections.Generic;
using UnityEngine;

public class PlayerOverlay : MonoBehaviour
{
    [SerializeField] PausePanel pausePanel;
    [SerializeField] InventoryPanel inventoryPanel;
    [SerializeField] ClassPanelUI classPanel;
    [SerializeField] RessourcePanel ressourcesPanel;
    [SerializeField] SkillsPanel skillsPanel;
    [SerializeField] ConsumablePanel consumablePanel;

    public PausePanel PausePanel => pausePanel;
    public RessourcePanel RessourcesPanel => ressourcesPanel;
    public SkillsPanel SkillsPanel => skillsPanel;
    public InventoryPanel InventoryPanel => inventoryPanel;
    public ClassPanelUI ClassPanel => classPanel;

    private void Awake()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InitEvent()
    {
        inventoryPanel.OnEquipConsumable += (_type, _item1, _item2) => SetConsumable(_type, _item1);
        inventoryPanel.OnDesequipConsumable += (_type, _item1, _item2) => ResetConsumable(_type);

        inventoryPanel.OnEquipEquipment += (_type,_item1, _item2) => classPanel.StatsPanel.UpdateData();
        inventoryPanel.OnDesequipEquipment += (_type,_item1, _item2) => classPanel.StatsPanel.UpdateData();

        classPanel.CompetencesPanel.OnSpellLearn += (_spell) => skillsPanel.LoadSkillIcon();
        classPanel.CompetencesPanel.OnSpellDesequip += (_spell) => skillsPanel.LoadSkillIcon();

        classPanel.CompetencesPanel.OnPassifLearn += (_passif) => skillsPanel.LoadPassifIcon();
        classPanel.CompetencesPanel.OnPassifDesequip += (_passif) => skillsPanel.LoadPassifIcon();
    }

    public void TogglePausePanel()
    {
        if (pausePanel.enabled)
        {
            Time.timeScale = 0.0f;
            pausePanel.gameObject.SetActive(true);
        }
        else
        {
            pausePanel.ResumeGame();
        }
    }

    public void OpenInventory() => inventoryPanel.Open();
    public void CloseInventory() => inventoryPanel.Close();

    public void OpenClassPanel() => classPanel.Open();
    public void CloseClassPanel() => classPanel.Close();

    public void UpdateConsumable(ItemStored _data)
    {
        EquipmentSlot _slot = inventoryPanel.GetEquipmentSlotFromData(_data);
        ConsumableSlot _consumable = consumablePanel.GetSlotFromData(_data);

        if (_data.amount == 0)
        {
            _slot.ClearItem();
            _consumable.ResetItem();
        }
        else
        {
            _slot.SetItem(_data);
            _consumable.SetItem(_data);
        }
    }

    public void SetConsumable(EquipmentType _type,ItemStored _data)
    {
        ConsumableSlot _consumable = consumablePanel.GetFromIndex((int)_type);
        _consumable.SetItem(_data);
    }

    public void ResetConsumable(EquipmentType _type)
    {
        ConsumableSlot _consumable = consumablePanel.GetFromIndex((int)_type);
        _consumable.ResetItem();
    }

    public void SetEquipedItemFromData(CharacterSaveData _data)
    {
        List<BaseItem> _allItems = ItemManager.Instance.AllItems;
        List<SaveItemData> _equipedItems = _data.itemEquiped;

        bool _firstConsumableEquiped = false;

        if (_equipedItems != null)
        {
            foreach (SaveItemData _itemData in _equipedItems)
            {
                foreach (BaseItem _item in _allItems)
                {
                    if (_item.ID == _itemData.ID)
                    {
                        ItemStored _newItem = new ItemStored(_item);
                        _newItem.amount = _itemData.amount;

                        if (_item.type == ItemType.CONSOMMABLE)
                        {
                            EquipmentType _type = _firstConsumableEquiped ? EquipmentType.CONSUMABLE_TWO : EquipmentType.CONSUMABLE_ONE;

                            SetConsumable(_type, _newItem);
                            inventoryPanel.PlayerInventoryRef.SetConsumable(_type, _newItem);
                            inventoryPanel.SetEquipmentFromData(_type, _newItem);

                            _firstConsumableEquiped = true;
                        }
                        else if (_item.type == ItemType.WEAPON)
                        {
                            inventoryPanel.SetEquipmentFromData(EquipmentType.WEAPON_RIGHT, _newItem);
                        }
                    }
                }
            }
        }
        classPanel.StatsPanel.UpdateData();
    }
}
