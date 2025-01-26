using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using Random = System.Random;


public class Deck : MonoBehaviour

{
    public Random RNG = new Random();
    public List<Card> deck;
    public List<Card> discard;
    public List<CardData> prefabs = new List<CardData>();

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
        Card drawnCard = deck[0];
        discard.Add(drawnCard);
        deck.RemoveAt(0);
        return drawnCard;
    }
    public void populateDeck()
    {
        int prefabIndex = 0;
        int chkMax = 0;
        int reqCardAmount = 5;
        for (int i = 0; i < reqCardAmount && chkMax < 45; i++, chkMax++)
        {
            CardData c = prefabs[prefabIndex];
            Card card = new Card(c.Type, c.Description);
            //Card card = prefabs[prefabIndex].GetComponent<Card>();
            deck.Add(card);
            if (i == reqCardAmount - 1)
            {
                prefabIndex++;
                reqCardAmount = 4;
            }
        }
    }
    public void shuffleDiscard()
    {
        deck = discard;
        discard.Clear();
        shuffleDeck();
    }
}
