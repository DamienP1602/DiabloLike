using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemToDrop
{
    public int percentageOfDrop;
    public Item itemToDrop;
    public bool mustDrop;
}

[CreateAssetMenu(fileName = "Empty Loot Table", menuName = "LootTable/Create new Loot Table")]
public class SO_LootTable : ScriptableObject
{
    public List<ItemToDrop> items = new();
}
