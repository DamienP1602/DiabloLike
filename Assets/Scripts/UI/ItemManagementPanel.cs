using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemManagementPanel : MonoBehaviour
{
    public ItemSlot itemClicked = null;
    [SerializeField] Button equipButton = null;
    [SerializeField] Button destroyItem = null;

    public void SetItemClick(ItemSlot _itemClicked) => itemClicked = _itemClicked;

    public void SetDestroyButtonEvent(UnityAction _event)
    {
        destroyItem.onClick.RemoveAllListeners();
        destroyItem.onClick.AddListener(_event);
    }
    public void SetEquipButtonEvent(UnityAction _event)
    {
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(_event);
    }
}
