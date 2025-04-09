using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellComponent : MonoBehaviour
{
    public event Action<Spell> OnLaunchSpell;
    public event Action<Spell> OnSpellReady;

    [Header("DEBUG")]
    [SerializeField] bool debugSpell;
    [SerializeField] List<Color> debugSpellsColor;

    [Header("Component")]
    [SerializeField] List<Spell> spells;
    [SerializeField] List<Spell> passifsSpells;
    [SerializeField] Transform spellSocket;
    AnimationComponent animRef;

    List<Spell> spellsOnCooldown;
    Spell currentSpell;

    public List<Spell> Spells => spells;
    public List<Spell> PassifsSpells => passifsSpells;


    // Start is called before the first frame update
    void Start()
    {
        animRef = GetComponent<AnimationComponent>();
        ResetSpells();
        spellsOnCooldown = new List<Spell>();
    }

    private void Update()
    {
        foreach (Spell _spell in spellsOnCooldown)
        {
            _spell.currentCooldown += Time.deltaTime;

            if (_spell.currentCooldown >= _spell.cooldown)
            {
                _spell.ResetSpell();
                spellsOnCooldown.Remove(_spell);
                OnSpellReady?.Invoke(_spell);
                break;
            }
        }
    }

    public void RemoveSpell(Spell _spellToRemove)
    {
        foreach (Spell _spellEquiped in spells)
        {
            if (_spellEquiped == _spellToRemove)
            {
                spells.Remove(_spellToRemove);
                _spellEquiped.ResetSpell();
                return;
            }
        }

    }

    public void AddSpell(Spell _spell)
    {
        spells.Add(_spell);
        _spell.ResetSpell();
    }

    void ResetSpells()
    {     
        foreach (Spell _spell in spells)
        {
            _spell.isOnCooldown = false;
            _spell.currentCooldown = 0.0f;
        }
    }

    public void LaunchSpell(int _index)
    {
        if (_index >= spells.Count)
            return;
        currentSpell = spells[_index];

        if (currentSpell.isOnCooldown)
            return;

        StatsComponent _stats = GetComponent<StatsComponent>();
        if (_stats.currentMana.Value < currentSpell.manaCost)
            return;

        //launch anim spell
        animRef.StartSpellAnimation(currentSpell.animationSpellName);

        Invoke(nameof(ExecuteSpell), currentSpell.castTime);
        Invoke(nameof(StopAnimation), currentSpell.castTime);

        spellsOnCooldown.Add(currentSpell);
        currentSpell.isOnCooldown = true;
        _stats.RemoveMana(currentSpell.manaCost);

        OnLaunchSpell?.Invoke(currentSpell);
    }

    void ExecuteSpell()
    {
        currentSpell.Execute(GetComponent<Player>(),spellSocket);
    }

    void StopAnimation()
    {
        animRef.StopSpellAnimation(currentSpell.animationSpellName);
    }

    private void OnDrawGizmos()
    {
        if (debugSpell)
        {
            if (debugSpellsColor.Count < spells.Count) return;

            for (int _i = 0; _i < spells.Count; _i++)
            {
                StandingAttackSpell _standingSpell = spells[_i] as StandingAttackSpell;
                if (!_standingSpell) continue;

                Gizmos.color = debugSpellsColor[_i];
                Gizmos.DrawWireSphere(transform.position,_standingSpell.range);

            }
        }
    }

    public void InitFromData(CharacterSaveData _data)
    {
        List<Spell> _allSpells = SpellManager.Instance.AllSpells;
        List<int> _savedIDSpells = _data.spellsIDEquiped;

        foreach (int _spellID in _savedIDSpells)
        {
            foreach (Spell _spell in _allSpells)
            {
                if (_spellID == _spell.ID)
                    spells.Add(_spell);
            }
        }

        List<int> _savedIDPassifs = _data.passifIDEquiped;

        foreach (int _passifID in _savedIDPassifs)
        {
            foreach (Spell _spell in _allSpells)
            {
                if (_passifID == _spell.ID)
                    spells.Add(_spell);
            }
        }
    }
}
