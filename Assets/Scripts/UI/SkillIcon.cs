using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpellState
{
    NOT_ENOUGH_MANA = 0,
    CANT_CAST = 1,
    NONE = 2
}

public class SkillIcon : MonoBehaviour
{
    [SerializeField] RawImage background;
    [SerializeField] RawImage icon;
    [SerializeField] RawImage foreground;

    public void SetBackground(Texture2D _backgroundTexture) => background.texture = _backgroundTexture;
    public void SetIcon(Texture2D _iconTexture) => icon.texture = _iconTexture;
    public void SetForeground(SpellState _state)
    {
        List<Color> _colors = new() { new Color(0.0f, 0.0f, 0.5f, 0.95f), new Color(0.4f, 0.4f, 0.4f, 0.85f), new Color(0.0f, 0.0f,0.0f,0.0f) };
        foreground.color = _colors[(int)_state];
    }
}
