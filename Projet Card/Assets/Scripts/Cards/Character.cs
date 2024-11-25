using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardValue
{
    int current = 0;
    [SerializeField] int max = 0;

    public int Current => current;
    public int Max => max;

    public CardValue(int _value)
    {
        Init(_value);
    }

    public void Init()
    {
        current = max;
    }

    public void Init(int _value)
    {
        max = _value;
        current = _value;
    }

    public void RemoveValue(int _value)
    {
        current -= _value;
        Clamp();
    }
    public void AddValue(int _value)
    {
        current += _value;
        Clamp();
    }

    void Clamp()
    {
        current = current > max ? max : current < 0 ? 0 : current;
    }

}

[Serializable]
public class Character
{
    public string characterName = "";
    public CardValue health = null;
    public CardValue damage = null;
    public List<string> races = new List<string>();

    public Effect effect = null;

    public Character(string _name, int _damage, int _health)
    {
        characterName = _name;
        damage = new CardValue(_damage);
        health = new CardValue(_health);

        effect = new Effect();
    }
}
