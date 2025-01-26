using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BoardManager board;
    public Card chosencard;
    public Canvas startMenu;
    public Canvas winMenu;
    public TMP_Text winText;
    public Canvas redTurn;
    public Canvas greenTurn;
    public Canvas blueTurn;
    public Canvas yellowTurn;
    public Canvas transitionScreen;
    public TurnManager turnManager;
    public Camera camera;
    public int selectedCard = -1;
    public bool variant = false;
    public Canvas variantSelectionScreen;
    public TMP_Text variantDescriptionA;
    public TMP_Text variantDescriptionB;

    public void TransitionPlayerScene()
    {
        if (transitionScreen.enabled == false)
        {
            redTurn.enabled = false;
            greenTurn.enabled = false;
            blueTurn.enabled = false;
            yellowTurn.enabled = false;
            transitionScreen.enabled = true;
        }
        else
        {
            transitionScreen.enabled = false;
            switch(turnManager.currentTurn)
            {
                case E_Color.Blue:
                    {
                        blueTurn.enabled = true;
                        break;
                    }
                case E_Color.Yellow:
                    {
                        yellowTurn.enabled = true;
                        break;
                    }
                case E_Color.Green:
                    {
                        greenTurn.enabled = true;
                        break;
                    }
                default:
                    redTurn.enabled = true;
                    break;
            }
        }
    }

    public Pawn SelectPawn()
    {
        Vector3 position = camera.ScreenToWorldPoint(Input.mousePosition);
        /*foreach(Pawn pawn in board.GetMoveablePawns(turnManager.currentTurn, turnManager.players[players[(int)currentTurn].playerHand.cards[selectedCard]))
         *{
         *  if(Vector3.Distance(pawn.gameObject.transform.position, position) < 1)
         *    {
         *      return pawn;
         *  }
         *}*/
        return null;
    }

    public void SelectCard(int index)
    {
        /*if(board.GetMoveablePawns(turnManager.currentTurn, players[(int)currentTurn].playerHand.cards[index]).Count > 0)
        *{
        *   selectedCard = index;
        *   int type = players[(int)currentTurn].playerHand.cards[selectedCard].Type;
        *   if(type == 10)
        *   {
        *       DisplayVariant("Move ten spaces forwards.", "Move one space backwards.");
        *   }
        *   else if(type == 11)
        *   {
        *       DisplayVariant("Move elecen spaces forwards.", "Swap the positions of one of your pieces and another piece.");
        *   }
        *}*/
    }

    public void DisplayVariant(string optionADescription, string optionBDescription)
    {
        variantDescriptionA.text = optionADescription;
        variantDescriptionB.text = optionBDescription;
        variantSelectionScreen.enabled = true;
    }

    public void SelectVariant(bool variantSelected)
    {
        variant = variantSelected;
        variantSelectionScreen.enabled = false;
    }

    public void SelectPlayerCount(byte count)
    {
        turnManager.playerCount = count;
        turnManager.EndTurn();
        while(turnManager.currentTurn != E_Color.Red)
        {
            turnManager.EndTurn();
        }
    }

    private void Start()
    {
        redTurn.enabled = false;
        greenTurn.enabled = false;
        blueTurn.enabled = false;
        yellowTurn.enabled = false;
        transitionScreen.enabled = false;
        variantSelectionScreen.enabled = false;
        winMenu.enabled = false;
    }

    public void DisplayWin()
    {
        string color = "";
        switch(turnManager.currentTurn)
        {
            case E_Color.Blue:
                {
                    color = "blue";
                    break;
                }
            case E_Color.Yellow:
                {
                    color = "yellow";
                    break;
                }
            case E_Color.Green:
                {
                    color = "green";
                    break;
                }
            default:
                {
                    color = "red";
                    break;
                }
        }
        winText.text = $"Congratulations, {color} has won the game!";
    }
}
