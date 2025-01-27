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
    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();
    public List<CardData> prefabs = new List<CardData>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        populateDeck();
        VerfyDeckPopulated();
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
        for (int i = 0; i < prefabs.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                CardData c = prefabs[i];
                Card card = new Card(c.Type, c.Description);
                deck.Add(card);
            }
        }
        deck.Add(new Card(prefabs[1].Type, prefabs[1].Description));
    }
    public void shuffleDiscard()
    {
        deck = discard;
        discard = new List<Card>();
        shuffleDeck();
    }

    public void VerfyDeckPopulated()
    {
        for (int i = 0; i < deck.Count(); i++)
        {
            Debug.Log(deck[i].ToString());
        }
        if (deck.Count() != 45)
        {
            Debug.LogError("Deck not populated!");
            return;
        }
        Debug.Log("Deck populated!");
    }
}
