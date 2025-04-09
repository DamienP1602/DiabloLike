using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public event Action<InventoryItem> OnItemClick;
    public event Action<InventoryItem> OnItemExecute;

    [SerializeField] RawImage rarityBackground;
    [SerializeField] RawImage itemSprite;
    [SerializeField] CustomButton button;
    [SerializeField] TMP_Text itemAmountText;

    [SerializeField] ItemStored slot;

    public ItemStored ItemData=> slot;
    public CustomButton Button => button;

    public bool IsAlreadyItemOnSlot => slot != null;

    protected virtual void Awake()
    {
        button.AddLeftClickAction(() => OnItemClick?.Invoke(this));
        button.AddRightClickAction(() => OnItemExecute?.Invoke(this));
    }

    public virtual void SetItem(ItemStored _data)
    {
        slot = _data;

        rarityBackground.color = _data.item.ratity.GetColorFromRarity();

        itemSprite.color = Color.white;
        itemSprite.texture = _data.item.icon;

        itemAmountText.text = _data.amount == 1 ? "" : _data.amount.ToString();
    }

    public virtual void ClearItem()
    {
        slot = null;

        rarityBackground.color = Color.clear;

        itemSprite.color = Color.clear;
        itemSprite.texture = null;

        itemAmountText.text = "";
        ReturnToStartingPos();
    }

    public void MoveFromMouse()
    {
        itemSprite.transform.position = Input.mousePosition;
    }

    public void ReturnToStartingPos()
    {
        itemSprite.transform.position = transform.position;
    }

    public void SetSelectionStatus(bool _value)
    {
        itemSprite.raycastTarget = _value;
        button.SetInteractable(_value);
    }
}
