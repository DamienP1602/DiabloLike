using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] GameObject itemDescription;
    [SerializeField] List<TMP_Text> allCreatedText;
    
    public void SetInformations(InventoryItem _slot)
    {
        transform.position = _slot.transform.position + new Vector3(150.0f,0.0f);
        gameObject.SetActive(true);
        itemName.text = _slot.ItemData.item.itemName;

        foreach (string _description in _slot.ItemData.item.ItemDescriptions)
        {
            TMP_Text _newText = Instantiate(itemName, itemDescription.transform);
            _newText.text = _description;
            allCreatedText.Add(_newText);
        }
    }

    public void Clear()
    {
        gameObject.SetActive(false);
        itemName.text = "";
        int _size = allCreatedText.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            TMP_Text _text = allCreatedText[_i];
            if (!_text) continue;

            Destroy(_text.gameObject);
        }
        allCreatedText.Clear();
    }
}
