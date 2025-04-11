using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    List<Spell> selectedSpells = new();
    [SerializeField] List<SkillIcon> icons = new();

    public bool HasEnoughMana(Spell _spell, float _currentMana) => _spell.manaCost <= _currentMana;

    public void InitSkillImages(List<Spell> _selectedSpells)
    {
        selectedSpells = _selectedSpells;

        LoadSkillImage();
    }

    public void LoadSkillImage()
    {
        int _iconsSize = icons.Count;
        for (int _i = 0; _i < _iconsSize; _i++)
        {
            if (_i >= selectedSpells.Count)
                icons[_i].ResetIcon();
            else
                icons[_i].SetIcon(selectedSpells[_i].icon);
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
            icons[_i].SetForeground(_state);
        }
    }

    public void StartSkillCooldown(Spell _spell, float _currentMana)
    {
        int _size = selectedSpells.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            if (_spell ==  selectedSpells[_i])
                icons[_i].SetForeground(HasEnoughMana(_spell, _currentMana) ? SpellState.CANT_CAST : SpellState.NOT_ENOUGH_MANA);
        }
    }
    public void StopSkillCooldown(Spell _spell, float _currentMana)
    {
        int _size = selectedSpells.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            if (_spell == selectedSpells[_i])
                icons[_i].SetForeground(HasEnoughMana(_spell, _currentMana) ? SpellState.NONE : SpellState.NOT_ENOUGH_MANA);
        }
    }
}
