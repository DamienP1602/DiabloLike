using UnityEngine;

public enum ItemType
{
    NONE = 0,
    CONSOMMABLE = 1,
    WEAPON = 2,
    ARMOR = 3 
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
