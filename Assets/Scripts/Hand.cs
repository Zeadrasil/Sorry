using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public List<Card> cards = new List<Card>();
    public Deck deck;

    public void AddCard()
    {
        cards.Add(deck.drawCard());
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

    public bool CheckPlayableCard(E_Color player, BoardManager boardManager)
    {
        foreach(Card card in cards)
        {
            if(boardManager.GetMoveablePawns(player, card).Count > 0)
            {
                return true;
            }
        }
        return false;
    }
}
