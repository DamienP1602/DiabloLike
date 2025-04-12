using System;
using System.Collections.Generic;
using UnityEngine;

public class CompetencesPanel : MonoBehaviour
{
    public Action<Spell> OnSpellLearn;
    public Action<Spell> OnSpellDesequip;

    public Action<Passif> OnPassifLearn;
    public Action<Passif> OnPassifDesequip;

    [SerializeField] CompetenceIcon competencePrefab;
    [SerializeField] List<CompetenceIcon> allCompetencesToLearn;

    [SerializeField] GameObject spellCategory;
    [SerializeField] GameObject passifCategory;

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
        List<SpellCharacterData<Spell>> _classSpells = _classData.allSpells;
        foreach (SpellCharacterData<Spell> _spellData in _classSpells)
        {
            CompetenceIcon _newCompetence = Instantiate(competencePrefab, spellCategory.transform);
            _newCompetence.SetSpell(_spellData.spell, _spellData.minimumLevel, _spellData.spell.icon);
            _newCompetence.Button.AddLeftClickAction(() => _newCompetence.LearnSpell(OnSpellLearn, OnSpellDesequip));

            allCompetencesToLearn.Add(_newCompetence);
        }

        List<SpellCharacterData<Passif>> _classPassifs = _classData.allPassifs;
        foreach (SpellCharacterData<Passif> _passifData in _classPassifs)
        {
            CompetenceIcon _newCompetence = Instantiate(competencePrefab, passifCategory.transform);
            _newCompetence.SetPassif(_passifData.spell, _passifData.minimumLevel, _passifData.spell.icon);
            _newCompetence.Button.AddLeftClickAction(() => _newCompetence.LearnPassif(OnPassifLearn, OnPassifDesequip));

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

    public void SetCompetenceFromData(CharacterSaveData _data)
    {
        List<SaveSpellData> _equipedSpells = _data.spellsEquiped;

        foreach (SaveSpellData _spell in _equipedSpells)
        {
            foreach (CompetenceIcon _comp in allCompetencesToLearn)
            {
                if (_comp.Spell)
                {
                    if (_comp.Spell.ID == _spell.ID)
                        _comp.SetIsLearn(true);
                }
            }
        }

        List<SaveSpellData> _equipedPassifs = _data.passifEquiped;
        foreach (SaveSpellData _passif in _equipedPassifs)
        {
            foreach (CompetenceIcon _comp in allCompetencesToLearn)
            {
                if (_comp.Passif)
                {
                    if (_comp.Passif.ID == _passif.ID)
                        _comp.SetIsLearn(true);
                }
            }
        }
    }
}
