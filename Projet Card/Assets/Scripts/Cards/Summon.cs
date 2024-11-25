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

        //test
        summon = new Character("Human Warrior",10,40);
        summon.effect.description = "Detruit une carte lorsqu'elle se fait toucher";
        summon.effect.effects.Add("Haste");
        summon.effect.effects.Add("Immediate");

        base.components.nameField.text = summon.characterName;
        base.components.damageField.text = summon.damage.Max.ToString();
        base.components.healthField.text = summon.health.Max.ToString();

        InitDescriptionAndEffectText();
    }

    void InitDescriptionAndEffectText()
    {
        Effect _effect = summon.effect;
        if (_effect == null) return;


        foreach(string _keyword in _effect.effects)
        {
            if(_keyword == "Cast" || _keyword == "Immediate" || _keyword == "Sacrifice")
            {
                base.components.effectField.text += _keyword + " :";
            }
            else
            {
                base.components.descriptionField.text += _keyword + ".\n";
            }
        }

        base.components.descriptionField.text += _effect.description;
    }
}
