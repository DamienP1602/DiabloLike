using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    IA_Player inputs = null;
    InputAction clickAction = null;

    InputAction pauseAction = null;
    InputAction inventoryAction = null;
    InputAction statsAction = null;

    InputAction ConsumableAction = null;

    InputAction qSpell = null;
    InputAction wSpell = null;
    InputAction eSpell = null;
    InputAction rSpell = null;

    InputAction spellsAction = null;


    public InputAction ClickAction => clickAction;
    public InputAction PauseAction => pauseAction;
    public InputAction InventoryAction => inventoryAction;
    public InputAction StatsAction => statsAction;
    public InputAction PotionAction => ConsumableAction;
    public InputAction QSpell => qSpell;
    public InputAction WSpell => wSpell;
    public InputAction ESpell => eSpell;
    public InputAction RSpell => rSpell;

    private void Awake()
    {
        inputs = new IA_Player();
    }
    private void OnEnable()
    {
        pauseAction = inputs.Player.Pause;
        ConsumableAction = inputs.Player.Consumable;
        inventoryAction = inputs.Player.Inventory;
        clickAction = inputs.Player.ClickInput;
        qSpell = inputs.Player.QSpell;
        wSpell = inputs.Player.WSpell;
        eSpell = inputs.Player.ESpell;
        rSpell = inputs.Player.RSpell;
        statsAction = inputs.Player.Stats;

        pauseAction.Enable();
        ConsumableAction.Enable();
        inventoryAction.Enable();
        statsAction.Enable();
        clickAction.Enable();
        qSpell.Enable();
        wSpell.Enable();
        eSpell.Enable();
        rSpell.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
        ConsumableAction.Disable();
        inventoryAction.Disable();
        statsAction.Disable();
        clickAction.Disable();
        qSpell.Disable();
        wSpell.Disable();
        eSpell.Disable();
        rSpell.Disable();
    }

}
