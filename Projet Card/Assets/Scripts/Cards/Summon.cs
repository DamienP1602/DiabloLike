using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Card
{
    [SerializeField] Character summon = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        summon.health.Init();
        summon.damage.Init();


        components.nameField.text = summon.characterName;
        components.damageField.text = summon.damage.Current.ToString();
        components.healthField.text = summon.health.Current.ToString();

        InitDescriptionAndEffectText();
    }

    void InitDescriptionAndEffectText()
    {
        Effect _effect = summon.effect;
        if (_effect == null) return;

        string[] _allKeywords =
        {
            "Cast",
            "Aura", 
            "Awake",
            "Power",
            "Death Wish",
            "Immediate",
            "Sacrifice", 
            "Profane",
            "Devotion" 
        };

        int _effectSize = _effect.effectNames.Count;
        for (int i = 0; i < _effectSize; i++)
        {
            string _keyword = _effect.effectNames[i];
            if (ContainsKeyWord(_keyword, _allKeywords))
                components.effectField.text += _keyword + " :";
            else
                components.descriptionField.text += _keyword + ".\n";
        }

        components.descriptionField.text += _effect.description;
    }

    bool ContainsKeyWord(string _word, string[] _list)
    {
        int _size = _list.Length;
        for (int i = 0; i < _size; i++)
        {
            if (_word.Contains(_list[i]))
                return true;
        }
        return false;
    }
}
