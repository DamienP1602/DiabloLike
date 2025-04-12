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
    [SerializeField] Passif passif;

    [SerializeField] int minimumLevel;
    [SerializeField] bool isLearned;

    public CustomButton Button => button;
    public int MinimumLevel => minimumLevel;

    public Spell Spell => spell;
    public Passif Passif => passif;

    public void SetSpell(Spell _spell, int _minimumLevel, Texture2D _icon)
    {
        spell = _spell;
        minimumLevel = _minimumLevel;
        icon.texture = _icon;
        levelText.text = _minimumLevel.ToString();
    }

    public void SetPassif(Passif _passif, int _minimumLevel, Texture2D _icon)
    {
        passif = _passif;
        minimumLevel = _minimumLevel;
        icon.texture = _icon;
        levelText.text = _minimumLevel.ToString();
    }

    public void SetInteractable(bool _value)
    {
        button.SetInteractable(_value);
        icon.color = _value ? new Color(1.0f,1.0f,1.0f) : new Color(0.3f, 0.3f,0.3f);
    }

    public void SetIsLearn(bool _value) => isLearned = _value;

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

    public void LearnPassif(Action<Passif> _learnAction, Action<Passif> _desequipAction)
    {
        if (isLearned)
        {
            _desequipAction(passif);
            isLearned = false;
            return;
        }

        isLearned = true;
        _learnAction?.Invoke(passif);
    }
}
