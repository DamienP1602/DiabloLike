using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceIcon : MonoBehaviour
{
    [SerializeField] CustomButton button;
    [SerializeField] TMP_Text levelText;
    [SerializeField] RawImage icon;

    [SerializeField] Spell spell;
    [SerializeField] int minimumLevel;
    [SerializeField] bool isLearned;

    public CustomButton Button => button;
    public int MinimumLevel => minimumLevel;

    // Start is called before the first frame update
    void Start()
    {
        icon.texture = spell.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpell(Spell _spell, int _minimumLevel, Texture2D _icon)
    {
        spell = _spell;
        minimumLevel = _minimumLevel;
        icon.texture = _icon;
        levelText.text = _minimumLevel.ToString();
    }

    public void SetInteractable(bool _value)
    {
        button.SetInteractable(_value);
        icon.color = _value ? new Color(1.0f,1.0f,1.0f) : new Color(0.3f, 0.3f,0.3f);
    }

    public void LearnSpell(Action<Spell> _learnAction, Action<Spell> _desequipAction)
    {
        if (isLearned)
        {
            _desequipAction(spell);
            isLearned = false;
            return;
        }

        isLearned = true;
        _learnAction?.Invoke(spell);
    }
}
