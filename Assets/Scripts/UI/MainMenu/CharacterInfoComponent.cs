using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoComponent : MonoBehaviour
{
    [SerializeField] CustomButton backgroundButton;
    [SerializeField] RawImage characterIcon;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text classText;

    public CustomButton Button => backgroundButton;
    public string CharacterName => nameText.text;

    private void Awake()
    {
        characterIcon.raycastTarget = false;
        levelText.raycastTarget = false;
        nameText.raycastTarget = false;
        classText.raycastTarget = false;
    }

    public void SetLevelText(string _text) => levelText.text = _text;
    public void SetNameText(string _text) => nameText.text = _text;
    public void SetClassText(string _text) => classText.text = _text;
    public void SetIcon(Texture2D _icon) => characterIcon.texture = _icon;
    public void SetClassTextColor(Color _color) => classText.color = _color;
}
