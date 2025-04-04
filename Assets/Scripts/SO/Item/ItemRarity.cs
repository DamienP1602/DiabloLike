using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY,
    SET,

    NONE
}

[Serializable]
public struct ItemRarity
{
    [SerializeField] Rarity rarity;

    public Color GetColorFromRarity()
    {
        List<Color> _colors = new() { Color.white, Color.yellow, Color.magenta, Color.red, Color.blue };
        return _colors[(int)rarity];
    }
}
