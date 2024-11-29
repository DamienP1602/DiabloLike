using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSide : MonoBehaviour
{
    const int BOARD_SIZE = 5;

    [SerializeField] string id;
    [SerializeField] List<BoardSlot> cardPositions = new List<BoardSlot>();
    [SerializeField] GameObject lifeAncer = null;
    [SerializeField] GameObject deckAncer = null;
    // Start is called before the first frame update
    void Start()
    {
        BoardManager.Instance.Add(this,id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckPosition(CardHandler _card)
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            BoardSlot _slot = cardPositions[i];
            if (Vector3.Distance(Input.mousePosition, _slot.transform.position) == 0.0f)
            {
                _slot.SetCard(_card);
                return true;
            }
                
        }
        return false;
    }

    
}
