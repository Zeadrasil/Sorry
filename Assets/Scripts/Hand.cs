using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public List<Card> cards = new List<Card>();
    public Deck deck;

    public void AddCard()
    {
        //cards.Add(deck.DrawCard());
    }

    public void DiscardCard(int cardIndex)
    {
        if (cardIndex > 3)
        {
            cards.Clear();
        }
        else
        {
            cards.RemoveAt(cardIndex);
        }
    }
}
