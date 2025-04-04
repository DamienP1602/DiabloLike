using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(CameraComponent),typeof(ClickComponent), typeof(HUD))]
[RequireComponent(typeof(InputComponent), typeof(Inventory), typeof(SpellComponent))]
public class Player : BaseCharacter
{
    CameraComponent cameraComp = null;
    ClickComponent clickComp = null;
    HUD hud = null;
    InputComponent inputs = null;
    Inventory inventory = null;
    SpellComponent spellComp = null;

    override protected void Start()
    {
        base.Start();
        SetInputs();
        SetEventOnUI();
    }

    void Update()
    {
        
    }

    override protected void Init() 
    {
        base.Init();
        cameraComp = GetComponent<CameraComponent>();
        clickComp = GetComponent<ClickComponent>();
        hud = GetComponent<HUD>();
        inputs = GetComponent<InputComponent>();
        inventory = GetComponent<Inventory>();
        spellComp = GetComponent<SpellComponent>();

        cameraComp.targetToFollow = transform;
        hud.Init(this);

        //Todo => Create Competence Tree from class when it will be selectable
        hud.Overlay.ClassPanel.CompetencesPanel.InitFromClass();
    }

    override protected void EventAssignation()
    {
        base.EventAssignation();

        clickComp.OnGround += movementComponent.SetDestination;
        clickComp.OnGround += (_target) => attackComponent.SetTarget(null);
        clickComp.OnGround += (_target) => movementComponent.SetTarget(null,0.0f);

        clickComp.OnTarget += attackComponent.SetTarget;
        clickComp.OnTarget += (_target) => movementComponent.SetTarget(_target, attackComponent.Range);

        attackComponent.OnKillTarget += () => movementComponent.SetTarget(null, 0.0f);
    }

    void SetEventOnUI()
    {
        statsComponent.OnLifeChange += hud.Overlay.RessourcesPanel.UpdateLifeValues;
        statsComponent.OnManaChange += hud.Overlay.RessourcesPanel.UpdateManaValues;
        statsComponent.OnManaChange += (_currentMana, _maxMana) => hud.Overlay.SkillsPanel.UpdateImagesFromMana(_currentMana);
        statsComponent.OnGainExperience += hud.Overlay.RessourcesPanel.UpdateExperienceValue;
        statsComponent.OnLevelUp += hud.Overlay.ClassPanel.CompetencesPanel.SetLevel;

        hud.Overlay.InventoryPanel.OnEquipConsumable += inventory.SetConsumable;
        hud.Overlay.InventoryPanel.OnEquipEquipment += inventory.EquipItem;

        hud.Overlay.ClassPanel.CompetencesPanel.OnSpellLearn += spellComp.AddSpell;


        inventory.OnConsumableUse += hud.Overlay.UpdateConsumable;

        spellComp.OnLaunchSpell += (_spell) => hud.Overlay.SkillsPanel.StartSkillCooldown(_spell,statsComponent.currentMana.Value);
        spellComp.OnSpellReady += (_spell) => hud.Overlay.SkillsPanel.StopSkillCooldown(_spell,statsComponent.currentMana.Value);

        hud.Overlay.InitEvent();
    }

    void SetInputs()
    {
        inputs.PotionAction.performed += inventory.UseConsumable;
        inputs.InventoryAction.performed += (_context) => hud.ToggleInventory();
        inputs.InventoryAction.performed += (_context) => clickComp.SetCanClick(hud.CanClick);
        inputs.StatsAction.performed += (_context) => hud.ToggleClassPanel();
        inputs.StatsAction.performed += (_context) => clickComp.SetCanClick(hud.CanClick);
        inputs.ClickAction.performed += (_context) => clickComp.RaycastOnClick();

        //Spells
        // Q = 0
        // W = 1
        // E = 2
        // R = 3
        inputs.QSpell.performed += (_context) => spellComp.LaunchSpell(0);
        inputs.WSpell.performed += (_context) => spellComp.LaunchSpell(1);
        inputs.ESpell.performed += (_context) => spellComp.LaunchSpell(2);
        inputs.RSpell.performed += (_context) => spellComp.LaunchSpell(3);

    }
}
