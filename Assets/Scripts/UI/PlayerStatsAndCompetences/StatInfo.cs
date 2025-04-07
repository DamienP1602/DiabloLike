using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatInfo : MonoBehaviour
{
    public event Action OnButtonClick = null; 
    [SerializeField] TMP_Text valueText = null;
    [SerializeField] Button addValueButton = null;

    public void Init(int _value, int _pointAmount)
    {
        valueText.text = _value.ToString();

        if (!addValueButton) return;
        addValueButton.gameObject.SetActive(_pointAmount == 0 ? false : true);
    }

    public void InitButton(Action _action)
    {
        addValueButton.onClick.AddListener(() => _action?.Invoke());
        addValueButton.onClick.AddListener(() => OnButtonClick?.Invoke());
    }

}
