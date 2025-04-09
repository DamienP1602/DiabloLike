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
    public int ID;
    public Player owner;
    public Texture2D icon;
    public string itemName ;
    public int itemPrice;
    public ItemRarity ratity;
    public ItemType type;

    public bool canBeStacked;

    public void InitOwner(Player _owner) => owner = _owner;

    public abstract void Execute();
}
