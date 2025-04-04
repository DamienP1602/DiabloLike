using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceIcon : MonoBehaviour
{
    [SerializeField] Spell spellToLear;
    [SerializeField] int minimumLevel;
    [SerializeField] Button button;
    [SerializeField] RawImage icon;
    [SerializeField] bool isLearned;

    public Button Button => button;
    public int MinimumLevel => minimumLevel;

    // Start is called before the first frame update
    void Start()
    {
        icon.texture = spellToLear.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInteractable(bool _value)
    {
        button.interactable = _value;
        icon.color = _value ? new Color(1.0f,1.0f,1.0f) : new Color(0.3f, 0.3f,0.3f);
    }

    public void LearnSpell(Action<Spell> _event)
    {
        if (isLearned) return;

        isLearned = true;
        _event?.Invoke(spellToLear);
    }
}
