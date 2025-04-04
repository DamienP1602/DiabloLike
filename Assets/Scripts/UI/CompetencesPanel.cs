using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompetencesPanel : MonoBehaviour
{
    public event Action<Spell> OnSpellLearn;

    [SerializeField] List<CompetenceIcon> allCompetencesToLearn;

    StatData level;

    private void Awake()
    {
        allCompetencesToLearn = GetComponentsInChildren<CompetenceIcon>().ToList();
        foreach (CompetenceIcon _comp in allCompetencesToLearn)
        {
            _comp.Button.onClick.AddListener(() => _comp.LearnSpell(OnSpellLearn));
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

    public void SetLevel(StatData _data) => level = _data;

    public void InitFromClass()
    {

    }

    public void Open()
    {
        foreach (CompetenceIcon _comp in allCompetencesToLearn)
        {
            _comp.SetInteractable(_comp.MinimumLevel > level.Value ? false : true);
        }
    }
}
