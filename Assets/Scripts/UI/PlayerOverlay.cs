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
        inventoryPanel.OnEquipConsumable += SetConsumable;
        inventoryPanel.OnEquipEquipment += (_item1, _item2) => classPanel.StatsPanel.UpdateData();

        classPanel.CompetencesPanel.OnSpellLearn += (_spell) =>  skillsPanel.LoadSkillImage();
        classPanel.CompetencesPanel.OnSpellDesequip += (_spell) =>  skillsPanel.LoadSkillImage();
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

    public void SetConsumable(int _index,ItemStored _data)
    {
        ConsumableSlot _consumable = consumablePanel.GetFromIndex(_index);
        _consumable.SetItem(_data);
    }
}
