using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct HoverEvent
{
    public float requiredTime;
    public Action hoverEvent;

    public HoverEvent(float _minimumTime, Action _action)
    {
        requiredTime = _minimumTime;
        hoverEvent = _action;
    }
}

[RequireComponent(typeof(RawImage))]
public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    event Action leftClickEvent;
    event Action rightClickEvent;
    event Action onEnterEvent;
    event Action onExitEvent;
    List<HoverEvent> allHoverEvents = new List<HoverEvent>();
    bool isHovered;
    bool isClicked;
    float hoveredTime;

    [SerializeField] bool interactable = true;
    [SerializeField] RawImage graphic;
    [SerializeField] Color baseColor = Color.white;
    [SerializeField] Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField] Color pressedColor = new Color(0.5f, 0.5f, 0.5f);
    [SerializeField] Color disabledColor = new Color(0.3f, 0.3f, 0.3f);

    public RawImage Graphic => graphic;

    private void Awake()
    {
        graphic = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        graphic.color = interactable ? baseColor : disabledColor;
    }

    protected virtual void Update()
    {
        if (!interactable) return;

        if (isHovered)
        {
            if (!isClicked)
                graphic.color = hoverColor;

            hoveredTime += Time.deltaTime;

            InvokeHoverEvent();
        }
    }

    public void AddLeftClickAction(Action _action) => leftClickEvent += _action;
    public void AddRightClickAction(Action _action) => rightClickEvent += _action;
    public void AddOnEnterAction(Action _action) => onEnterEvent += _action;
    public void AddOnExitAction(Action _action) => onExitEvent += _action;
    public void AddHoverAction(Action _action, float _minimumTime) => allHoverEvents.Add(new HoverEvent(_minimumTime, _action));

    void InvokeLeftClick() => leftClickEvent?.Invoke();
    void InvokeRightClick() => rightClickEvent?.Invoke();
    void InvokeOnEnter() => onEnterEvent?.Invoke();
    void InvokeOnExit() => onExitEvent?.Invoke();

    void InvokeHoverEvent()
    {
        foreach (HoverEvent _data in allHoverEvents)
        {
            if (_data.requiredTime <= hoveredTime)
                _data.hoverEvent?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        graphic.color = pressedColor;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InvokeLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            InvokeRightClick();
        }
        isClicked = true;
        Invoke(nameof(StopClicked), 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactable) return;

        graphic.color = hoverColor;
        isHovered = true;

        InvokeOnEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) return;

        graphic.color = baseColor;
        isHovered = false;
        hoveredTime = 0.0f;

        InvokeOnExit();
    }


    public void SetInteractable(bool _value)
    {
        interactable = _value;
        graphic.color = _value ? baseColor : disabledColor;
    }

    void StopClicked()
    {
        isClicked = false;
    }
}
