using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellComponent : MonoBehaviour
{
    public event Action<Spell> OnLaunchSpell;
    public event Action<Spell> OnSpellReady;

    [Header("DEBUG")]
    [SerializeField] bool debugSpell;
    [SerializeField] List<Color> debugSpellsColor;

    [Header("Component")]
    [SerializeField] List<Spell> spells;
    [SerializeField] List<Passif> passifsSpells;
    [SerializeField] Transform spellSocket;

    AnimationComponent animRef;
    MovementComponent movementRef;
    CameraComponent cameraRef;
    Player playerRef;

    List<Spell> spellsOnCooldown;
    Spell currentSpell;

    public List<Spell> Spells => spells;
    public List<Passif> Passifs => passifsSpells;


    // Start is called before the first frame update
    void Start()
    {
        animRef = GetComponent<AnimationComponent>();
        movementRef = GetComponent<MovementComponent>();
        cameraRef = GetComponent<CameraComponent>();
        playerRef = GetComponent<Player>();
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

        foreach (Passif _passif in passifsSpells)
        {
            if (_passif.IsAlwaysActive) continue;

            _passif.currentCooldown += Time.deltaTime;
            if (_passif.currentCooldown >= _passif.cooldown)
            {
                _passif.currentCooldown = 0.0f;
                _passif.Activate(playerRef);
            }
        }
    }

    public void SetSpellSocket()
    {
        SpellSocket _spellSocket = GetComponentInChildren<SpellSocket>(true);
        if (_spellSocket == null) return;

        spellSocket = _spellSocket.transform;
    }

    public void RemoveSpell(Spell _spellToRemove)
    {
        spells.Remove(_spellToRemove);
        _spellToRemove.ResetSpell();
    }

    public void AddSpell(Spell _spell)
    {
        spells.Add(_spell);
        _spell.ResetSpell();
    }

    public void AddPassif(Passif _passif)
    {
        passifsSpells.Add(_passif);
        _passif.currentCooldown = 0.0f;

        if (_passif.IsAlwaysActive)
            _passif.Activate(playerRef);
    }

    public void RemovePassif(Passif _passif)
    {
        passifsSpells.Remove(_passif);
        if (_passif.IsAlwaysActive)
            _passif.Desactivate(playerRef);
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

        if (currentSpell.needToRotatePlayer)
        {
            RotateMeshFromMousePosition();
            movementRef.SetCantMove(currentSpell.castTime);
        }

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
        currentSpell.Execute(playerRef,spellSocket);
        movementRef.SetRotationTarget(false);
    }

    void StopAnimation()
    {
        animRef.StopSpellAnimation(currentSpell.animationSpellName);
    }

    void RotateMeshFromMousePosition()
    {
        Ray _ray = cameraRef.RenderCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _result;

        if (Physics.Raycast(_ray, out _result, 100.0f, GetComponent<ClickComponent>().GroundLayer))
        {
            movementRef.SetRotationTarget(true,_result.point);
        }
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
        List<SaveSpellData> _savedIDSpells = _data.spellsEquiped;

        foreach (SaveSpellData _spellData in _savedIDSpells)
        {
            foreach (Spell _spell in _allSpells)
            {
                if (_spellData.ID == _spell.ID)
                    spells.Add(_spell);
            }
        }

        List<Passif> _allPassifs = SpellManager.Instance.AllPassifs;
        List<SaveSpellData> _savedIDPassifs = _data.passifEquiped;

        foreach (SaveSpellData _passifData in _savedIDPassifs)
        {
            foreach (Passif _passif in _allPassifs)
            {
                if (_passifData.ID == _passif.ID)
                    passifsSpells.Add(_passif);
            }
        }
    }
}
