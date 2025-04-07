using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct RessourceData
{
    public Slider slider;
    public TMP_Text text;
}

public class RessourcePanel : MonoBehaviour
{
    [SerializeField] RessourceData health;
    [SerializeField] RessourceData mana;
    [SerializeField] Slider experience;

    public void UpdateLifeValues(float _actualValue, float _maxValue)
    {
        float _value = _actualValue / _maxValue;

        health.slider.value = _value;
        health.text.text = _actualValue.ToString();
    }

    public void UpdateManaValues(float _actualValue, float _maxValue)
    {
        float _value = _actualValue / _maxValue;
        
        mana.slider.value = _value;
        mana.text.text = _actualValue.ToString();
    }

    public void UpdateExperienceValue(float _actualValue, float _maxValue)
    {
        float _value = _actualValue / _maxValue;
        experience.value = _value;
    }
}
