using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropComponent : MonoBehaviour
{
    [SerializeField] SO_LootTable lootTable;
    [SerializeField] int minGoldDropAmount;
    [SerializeField] int maxGoldDropAmount;

    public void ComputeLootTable()
    {
        int _goldAmount = Random.Range(minGoldDropAmount, maxGoldDropAmount + 1);
        GameManager.Instance.Player.Inventory.ChangeGoldValue(_goldAmount);

        if (lootTable.items.Count == 0) return;

        foreach (ItemToDrop _itemInfo in lootTable.items)
        {
            int _dropNumber = Random.Range(1, 101);

            if (_dropNumber <= _itemInfo.percentageOfDrop || _itemInfo.mustDrop)
            {
                SpawnItem(_itemInfo.itemToDrop);
            }
        }
    }

    public void SpawnItem(Item _itemToSpawn)
    {
        Item _item = Instantiate(_itemToSpawn, transform.position, Quaternion.identity);
        float _x = Mathf.Cos(Time.time) * 1.0f;
        float _z = Mathf.Sin(Time.time) * 1.0f;
        _item.transform.position = transform.position + new Vector3(_x,0.0f,_z);
    }
}
