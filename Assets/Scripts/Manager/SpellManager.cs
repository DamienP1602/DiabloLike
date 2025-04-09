using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    [SerializeField] List<Spell> allSpells;

    public List<Spell> AllSpells => allSpells;

    protected override void Awake()
    {
        base.Awake();

        foreach (Spell _spell in allSpells)
        {
            _spell.ResetSpell();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
