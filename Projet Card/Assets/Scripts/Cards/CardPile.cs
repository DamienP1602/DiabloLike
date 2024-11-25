using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPile : MonoBehaviour
{
    [SerializeField] protected List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(Card _card)
    {
        cards.Add(_card);
    }

    public void RemoveCard(Card _card)
    {
        cards.Remove(_card);
    }
}
