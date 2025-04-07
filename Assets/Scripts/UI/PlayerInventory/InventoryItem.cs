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

    [SerializeField] RawImage rarityBackground;
    [SerializeField] RawImage itemSprite;
    [SerializeField] Button button;
    [SerializeField] TMP_Text itemAmountText;

    [SerializeField] ItemStored slot;

    public ItemStored Item => slot;
    public Button Button => button;

    public bool IsAlreadyItemOnSlot => slot != null;

    protected virtual void Awake()
    {
        RawImage[] _images = GetComponentsInChildren<RawImage>();
        rarityBackground = _images[0];
        itemSprite = _images[1];

        button = GetComponentInChildren<Button>();
        itemAmountText = GetComponentInChildren<TMP_Text>();

        button.onClick.AddListener(() => OnItemClick?.Invoke(this));
        itemSprite.color = Color.clear;
        itemAmountText.text = "";
        slot = null;
    }

    public virtual bool SetItem(ItemStored _data)
    {
        slot = _data;

        rarityBackground.color = _data.item.ratity.GetColorFromRarity();

        itemSprite.color = Color.white;
        itemSprite.texture = _data.item.icon;

        itemAmountText.text = _data.amount == 1 ? "" : _data.amount.ToString();

        return true;
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
        button.targetGraphic.raycastTarget = _value;
    }
}
