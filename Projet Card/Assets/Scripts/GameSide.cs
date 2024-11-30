using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameSide : MonoBehaviour
{
    const int BOARD_SIZE = 5;

    [SerializeField] string id;
    [SerializeField] List<BoardSlot> cardPositions = new List<BoardSlot>();
    [SerializeField] DeckPile deck = null;

    public string ID => id;

    void Start()
    {
        init();
    }

    // Update is called once per frame
    void init()
    {
        BoardManager.Instance.Add(this,id);
        deck = GetComponentInChildren<DeckPile>();
        deck.SetSide(this);
    }

    public bool CheckPosition(CardHandler _card)
    {
        //BOARD_SIZE
        for (int i = 0; i < 1; i++)
        {
            BoardSlot _slot = cardPositions[i];
            if (_slot.HasCard) continue;

            if (Overlaps(_slot))
            {
                _slot.SetCard(_card);
                return true;
            } 
        }
        return false;
    }

    bool Overlaps(BoardSlot _slot)
    {
        Rect _mouseRect = new Rect(Input.mousePosition, new Vector2(1.0f, 1.0f));
        Vector2 _slotSize = new Vector2(200.0f, 300.0f);
        Vector2 _slotPosition = (Vector2)_slot.transform.position - (_slotSize / 2.0f);
        Rect _slotRect = new Rect(_slotPosition, new Vector2(200.0f, 300.0f));

        return _slotRect.Overlaps(_mouseRect); 
    }

}
