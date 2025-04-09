using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] PlayerOverlay overlayprefab;
    bool inventoryOpen = false;
    bool statsOpen = false;

    public PlayerOverlay Overlay { get; private set; }

    public bool IsInOverlay => statsOpen || inventoryOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Player _player)
    {
        Overlay = Instantiate(overlayprefab, transform);

        StatsComponent _stats = _player.GetComponent<StatsComponent>();
        Overlay.RessourcesPanel.UpdateLifeValues(_stats.currentHealth.Value, _stats.maxHealth.Value);
        Overlay.RessourcesPanel.UpdateManaValues(_stats.currentMana.Value, _stats.maxMana.Value);
        Overlay.RessourcesPanel.UpdateExperienceValue(_stats.experience.Value, _stats.experienceCap.Value);

        Overlay.ClassPanel.StatsPanel.SetStats(_stats);
        Overlay.ClassPanel.StatsPanel.Init();
        Overlay.ClassPanel.CompetencesPanel.SetLevel(_stats.level);

        Overlay.InventoryPanel.Init();
        Overlay.InventoryPanel.SetInventory(_player.GetComponent<Inventory>());

        SpellComponent _spells = _player.GetComponent<SpellComponent>();
        Overlay.SkillsPanel.InitSkillImages(_spells.Spells);

        Overlay.PausePanel.SetPlayerRef(_player);
    }

    public void ToggleInventory()
    {
        Player _player = GetComponent<Player>();

        if (inventoryOpen)
        {
            Overlay.CloseInventory();
            inventoryOpen = false;
        }
        else
        {
            Overlay.OpenInventory();
            inventoryOpen = true;
        }
    }

    public void ToggleClassPanel()
    {
        Player _player = GetComponent<Player>();

        if (statsOpen)
        {
            Overlay.CloseClassPanel();
            statsOpen = false;
        }
        else
        {
            Overlay.OpenClassPanel();
            statsOpen = true;
        }
    }
}
