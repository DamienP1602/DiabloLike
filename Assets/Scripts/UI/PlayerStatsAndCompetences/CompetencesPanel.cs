using System;
using System.Collections.Generic;
using UnityEngine;

public class CompetencesPanel : MonoBehaviour
{
    public Action<Spell> OnSpellLearn;
    public Action<Spell> OnSpellDesequip;

    [SerializeField] CompetenceIcon competencePrefab;
    [SerializeField] List<CompetenceIcon> allCompetencesToLearn;    

    StatData level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(StatData _data) => level = _data;

    public void InitFromClassData(SO_CharacterClass _classData)
    {
        List<SpellCharacterData> _classSpells = _classData.allSpells;
        foreach (SpellCharacterData _spellData in _classSpells)
        {
            CompetenceIcon _newCompetence = Instantiate(competencePrefab, transform);
            _newCompetence.SetSpell(_spellData.spell, _spellData.minimumLevel, _spellData.spell.icon);
            _newCompetence.Button.AddLeftClickAction(() => _newCompetence.LearnSpell(OnSpellLearn, OnSpellDesequip));

            allCompetencesToLearn.Add(_newCompetence);
        }
    }

    public void Open()
    {
        foreach (CompetenceIcon _comp in allCompetencesToLearn)
        {
            _comp.SetInteractable(_comp.MinimumLevel > level.Value ? false : true);
        }
    }
}
