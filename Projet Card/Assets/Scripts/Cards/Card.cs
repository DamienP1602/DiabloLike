using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct UIComponents
{
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI healthField;
    public TextMeshProUGUI damageField;
    public TextMeshProUGUI effectField;
    public TextMeshProUGUI descriptionField;
    public Image sprite;
}


public abstract class Card : MonoBehaviour
{
    [SerializeField] protected UIComponents components;
    [SerializeField] Sprite cardSprite = null;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        components.sprite.sprite = cardSprite;
    }
}
