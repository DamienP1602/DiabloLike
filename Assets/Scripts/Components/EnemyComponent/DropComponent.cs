using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropComponent : MonoBehaviour
{
    [SerializeField] SO_LootTable lootTable = null;

    public void ComputeLootTable()
    {
        if (lootTable.items.Count == 0) return;

        foreach (ItemToDrop _itemInfo in lootTable.items)
        {
            int _dropNumber = UnityEngine.Random.Range(1, 101);

            if (_dropNumber <= _itemInfo.percentageOfDrop || _itemInfo.mustDrop)
            {
                SpawnItem(_itemInfo.itemToDrop);
            }
        }
    }

    public void SpawnItem(Item _itemToSpawn)
    {
        Item _item = Instantiate(_itemToSpawn, transform.position, Quaternion.identity);
    }
}
