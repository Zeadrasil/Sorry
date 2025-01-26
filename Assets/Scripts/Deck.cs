using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;


public class Deck : MonoBehaviour

{   public Random RNG = new Random();
    public List<Card> deck;
    public List<Card> discard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        populateDeck();
        shuffleDeck();
    }

    // Update is called once per frame
    void Update()
    {
        if (deck.Count() == 0)
        {
            shuffleDiscard();

        }
    }


    public void shuffleDeck()
    {
        Random rng = new Random();
        for (int i = deck.Count() - 1; i > 0; i--)
        {
            int randomIndex = rng.Next(0, i + 1);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
    public Card drawCard()
    {
        return deck[0];
    }
    public  void populateDeck()
    {
        int cardNumber = 1;
        int chkMax = 0;
        int reqCardAmount = 5;
        for (int i = 0; i < reqCardAmount && chkMax < 45; i++, chkMax++)
        {
            deck.Add(new Card(cardNumber, null));
            if (cardNumber == 6 || cardNumber == 9)
            {
                cardNumber += 2;
            }
            else cardNumber += 1;
        }
    }
    public void shuffleDiscard()
    {
        deck = discard;
        discard.Clear();
        shuffleDeck();
    }
}
