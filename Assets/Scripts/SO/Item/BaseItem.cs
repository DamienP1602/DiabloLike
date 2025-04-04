using UnityEngine;

public enum ItemType
{
    CONSOMMABLE,
    WEAPON,
    ARMOR
}

public abstract class BaseItem : ScriptableObject
{
    public Player owner = null;
    public Texture2D icon = null;
    public string itemName = "";
    public int itemPrice = 0;
    public ItemRarity ratity;
    public ItemType type;

    public bool canBeStacked = true;

    public void InitOwner(Player _owner) => owner = _owner;

    public abstract void Execute();
}
