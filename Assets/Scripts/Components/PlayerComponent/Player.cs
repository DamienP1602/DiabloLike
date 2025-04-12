using UnityEngine;

[RequireComponent(typeof(CameraComponent),typeof(ClickComponent), typeof(HUD))]
[RequireComponent(typeof(InputComponent), typeof(Inventory), typeof(SpellComponent))]
[RequireComponent(typeof(ClassComponent),typeof(InteractComponent))]
public class Player : BaseCharacter
{
    CameraComponent cameraComp;
    ClickComponent clickComp;
    HUD hud;
    InputComponent inputs;
    Inventory inventory;
    SpellComponent spellComp;
    ClassComponent classComp;
    InteractComponent interactComp;

    public CameraComponent CameraComponent => cameraComp;
    public ClickComponent ClickComponent => clickComp;
    public HUD HUD => hud;
    public InputComponent InputsComponent => inputs;
    public Inventory Inventory => inventory;
    public SpellComponent SpellComponent => spellComp;
    public ClassComponent ClassComponent => classComp;
    public InteractComponent InteractComponent => interactComp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }

    override protected void Init() 
    {
        base.Init();

        clickComp = GetComponent<ClickComponent>();

        inputs = GetComponent<InputComponent>();
        inventory = GetComponent<Inventory>();
        spellComp = GetComponent<SpellComponent>();
        classComp = GetComponent<ClassComponent>();
        interactComp = GetComponent<InteractComponent>();
    }

    override protected void EventAssignation()
    {
        base.EventAssignation();

        clickComp.OnGround += movementComponent.SetDestination;
        clickComp.OnGround += (_target) => attackComponent.SetTarget(null);
        clickComp.OnGround += (_target) => movementComponent.SetTarget(null,0.0f);
        clickComp.OnGround += (_target) => interactComp.SetTarget(null);

        clickComp.OnTarget += attackComponent.SetTarget;
        clickComp.OnTarget += (_target) => movementComponent.SetTarget(_target, attackComponent.Range);

        clickComp.OnGPE += (_interactable) =>  movementComponent.SetTarget(_interactable.gameObject,_interactable.Range - 1.0f);
        clickComp.OnGPE += interactComp.SetTarget;

        attackComponent.OnKillTarget += () => movementComponent.SetTarget(null, 0.0f);
    }


    public void InitPlayer(CharacterSaveData _data, SO_CharacterClass _classData)
    {
        base.Start();
        
        cameraComp = GetComponent<CameraComponent>();
        hud = GetComponent<HUD>();

        characterName = _data.name;
        Instantiate(_classData.characterPrevisualisation.Body,transform);
        animationComponent.SetAnimatorController(_classData.controller);

        classComp.SetCharacterClass(_classData);
        statsComponent.InitFromData(_data);
        statsComponent.ComputeNextExperienceCap();
        hud.Init(this);
        inventory.InitFromData(_data);
        inventory.SetGoldAmount(_data.gold);
        spellComp.InitFromData(_data);
        hud.Overlay.SetEquipedItemFromData(_data);
        hud.Overlay.ClassPanel.CompetencesPanel.InitFromClassData(_classData);
        hud.Overlay.ClassPanel.CompetencesPanel.SetCompetenceFromData(_data);
        hud.Overlay.InventoryPanel.SetGoldText(_data.gold);

        hud.Overlay.SkillsPanel.LoadSkillIcon();
        hud.Overlay.SkillsPanel.LoadPassifIcon();
        spellComp.SetSpellSocket();

        cameraComp.CreateCamera();

        SetEventOnUI();
        SetInputs();
    }

    void SetEventOnUI()
    {
        statsComponent.OnLifeChange += hud.Overlay.RessourcesPanel.UpdateLifeValues;
        statsComponent.OnManaChange += hud.Overlay.RessourcesPanel.UpdateManaValues;
        statsComponent.OnManaChange += (_currentMana, _maxMana) => hud.Overlay.SkillsPanel.UpdateImagesFromMana(_currentMana);
        statsComponent.OnGainExperience += hud.Overlay.RessourcesPanel.UpdateExperienceValue;
        statsComponent.OnLevelUp += hud.Overlay.ClassPanel.CompetencesPanel.SetLevel;

        hud.Overlay.InventoryPanel.OnEquipConsumable += inventory.SetConsumable;
        hud.Overlay.InventoryPanel.OnDesequipConsumable += (_type, _nullItem, _currentConsumable) => inventory.RemoveConsumable(_type, _currentConsumable);

        hud.Overlay.InventoryPanel.OnEquipEquipment += inventory.EquipItem;
        hud.Overlay.InventoryPanel.OnDesequipEquipment += (_type, _nullItem, _currentItem) => inventory.DesequipItem(_type,_currentItem);

        hud.Overlay.ClassPanel.CompetencesPanel.OnSpellLearn += spellComp.AddSpell;
        hud.Overlay.ClassPanel.CompetencesPanel.OnSpellDesequip += spellComp.RemoveSpell;

        hud.Overlay.ClassPanel.CompetencesPanel.OnPassifLearn += spellComp.AddPassif;
        hud.Overlay.ClassPanel.CompetencesPanel.OnPassifDesequip += spellComp.RemovePassif;


        inventory.OnConsumableUse += hud.Overlay.UpdateConsumable;
        inventory.OnChangeGoldAmount += hud.Overlay.InventoryPanel.SetGoldText;

        spellComp.OnLaunchSpell += (_spell) => hud.Overlay.SkillsPanel.StartSkillCooldown(_spell,statsComponent.currentMana.Value);
        spellComp.OnSpellReady += (_spell) => hud.Overlay.SkillsPanel.StopSkillCooldown(_spell,statsComponent.currentMana.Value);

        hud.Overlay.InitEvent();
    }

    void SetInputs()
    {
        inputs.PauseAction.performed += (_context) => hud.Overlay.TogglePausePanel();
        inputs.PotionAction.performed += inventory.UseConsumable;
        inputs.InventoryAction.performed += (_context) => hud.ToggleInventory();
        inputs.InventoryAction.performed += (_context) => clickComp.SetOverlayMode(hud.IsInOverlay);
        inputs.StatsAction.performed += (_context) => hud.ToggleClassPanel();
        inputs.StatsAction.performed += (_context) => clickComp.SetOverlayMode(hud.IsInOverlay);
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
