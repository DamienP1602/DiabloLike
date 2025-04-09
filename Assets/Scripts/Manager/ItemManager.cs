using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] List<BaseItem> allItems;
    [SerializeField] List<Item> allItemsOnGround;

    public List<BaseItem> AllItems => allItems;

    public void AddItem(Item _item)
    {
        allItemsOnGround.Add(_item);
    }

    public void RemoveItem(Item _item)
    {
        allItemsOnGround.Remove(_item);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
