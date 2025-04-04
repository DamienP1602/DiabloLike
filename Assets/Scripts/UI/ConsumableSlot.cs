using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableSlot : MonoBehaviour
{
    [SerializeField] ItemStored data;
    [SerializeField] RawImage sprite;

    public ItemStored Item => data;

    private void Awake()
    {
        sprite.color = Color.clear;
    }

    public void SetItem(ItemStored _data)
    {
        data = _data;
        sprite.color = Color.white;
        sprite.texture = _data.item.icon;
    }

    public void ResetItem()
    {
        data.item = null;
        sprite.color = Color.clear;
        sprite.texture = null;
    }
}
