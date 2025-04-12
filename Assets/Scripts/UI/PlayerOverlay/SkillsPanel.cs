using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    List<Spell> selectedSpells = new();
    List<Passif> selectedPassifs = new();

    [SerializeField] List<SkillIcon> spellIcons = new();
    [SerializeField] List<SkillIcon> passifIcons = new();

    public bool HasEnoughMana(Spell _spell, float _currentMana) => _spell.manaCost <= _currentMana;

    public void InitSkillImages(List<Spell> _selectedSpells, List<Passif> _selectedPassifs)
    {
        selectedSpells = _selectedSpells;
        selectedPassifs = _selectedPassifs;

        LoadSkillIcon();
        LoadPassifIcon();
    }

    public void LoadSkillIcon()
    {
        int _iconsSize = spellIcons.Count;
        for (int _i = 0; _i < _iconsSize; _i++)
        {
            if (_i >= selectedSpells.Count)
                spellIcons[_i].ResetIcon();
            else
                spellIcons[_i].SetIcon(selectedSpells[_i].icon);
        }
    }

    public void LoadPassifIcon()
    {
        int _iconsSize = passifIcons.Count;
        for (int _i = 0; _i < _iconsSize; _i++)
        {
            if (_i >= selectedPassifs.Count)
                passifIcons[_i].ResetIcon();
            else
                passifIcons[_i].SetIcon(selectedPassifs[_i].icon);
        }
    }

    public void UpdateImagesFromMana(float _currentManaAmount)
    {
        int _selectedSpellsSize = selectedSpells.Count;
        for (int _i = 0; _i < _selectedSpellsSize; _i++)
        {
            Spell _spell = selectedSpells[_i];
            if (!_spell)
                return;

            SpellState _state = HasEnoughMana(_spell,_currentManaAmount) ? SpellState.NONE : SpellState.NOT_ENOUGH_MANA;
            spellIcons[_i].SetForeground(_state);
        }
    }

    public void StartSkillCooldown(Spell _spell, float _currentMana)
    {
        int _size = selectedSpells.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            if (_spell ==  selectedSpells[_i])
                spellIcons[_i].SetForeground(HasEnoughMana(_spell, _currentMana) ? SpellState.CANT_CAST : SpellState.NOT_ENOUGH_MANA);
        }
    }
    public void StopSkillCooldown(Spell _spell, float _currentMana)
    {
        int _size = selectedSpells.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            if (_spell == selectedSpells[_i])
                spellIcons[_i].SetForeground(HasEnoughMana(_spell, _currentMana) ? SpellState.NONE : SpellState.NOT_ENOUGH_MANA);
        }
    }
}
