using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int Type;
    public string Description;

    public Card(int type, string description)
    {
        Type = type;
        Description = description;
    }
}
