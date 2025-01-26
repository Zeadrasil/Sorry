using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public byte playerCount = 4;
    public Player[] players = new Player[4];
    public E_Color currentTurn = E_Color.Red;
    public BoardManager boardManager;

    private void Start()
    {
        for (int index = 0; index < players.Length; index++)
        {
            players[index] = new Player();
        }
        players[0].color = currentTurn;
        players[0].playerHand.deck = boardManager.deck;
        players[1].color = E_Color.Blue;
        players[1].playerHand.deck = boardManager.deck;
        players[2].color = E_Color.Yellow;
        players[2].playerHand.deck = boardManager.deck;
        players[3].color = E_Color.Green;
        players[3].playerHand.deck = boardManager.deck;
    }


    public void EndTurn()
    {
        while (players[(int)currentTurn].playerHand.cards.Count < 4)
        {
            players[(int)currentTurn].playerHand.AddCard();
        }
        switch(currentTurn)
        {
            case E_Color.Red:
                {
                    if(playerCount == 2)
                    {
                        currentTurn = E_Color.Yellow;
                    }
                    else
                    {
                        currentTurn = E_Color.Blue;
                    }
                    break;
                }
            case E_Color.Blue:
                {
                    currentTurn = E_Color.Yellow;
                    break;
                }
            case E_Color.Yellow:
                {
                    if(playerCount == 2 || playerCount == 3)
                    {
                        currentTurn = E_Color.Red;
                    }
                    else
                    {
                        currentTurn = E_Color.Green;
                    }
                    break;
                }
            default:
                {
                    currentTurn = E_Color.Red; 
                    break;
                }
        }
    }
}
