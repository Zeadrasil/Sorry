using System;
using UnityEngine;

public class Card
{
    public int Type;
    public string Description;

    public Card(int type, string description)
    {
        Type = type;
        Description = description;
    }

    public override string ToString()
    {
        if(Type == 0)
        {
            return "Sorry!";
        }
        return Type.ToString();
    }
}
