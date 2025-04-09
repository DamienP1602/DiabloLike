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
}
