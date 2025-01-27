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
    public new Camera camera;
    public int selectedCard = -1;
    public int variant = -1;
    public Canvas variantSelectionScreen;
    public Canvas sevenSelectionScreen;
    public TMP_Text variantDescriptionA;
    public TMP_Text variantDescriptionB;
    public TMP_Text[] redCardLabels;
    public TMP_Text[] greenCardLabels;
    public TMP_Text[] blueCardLabels;
    public TMP_Text[] yellowCardLabels;
    public TMP_Text[] redCardDescriptions;
    public TMP_Text[] greenCardDescriptions;
    public TMP_Text[] blueCardDescriptions;
    public TMP_Text[] yellowCardDescriptions;
    public TMP_Text[,,] cardDataHolders;
    public Canvas[] cardSpriteHolderSections;
    public Canvas beforeCardSelect;
    public Canvas afterCardSelect;
    public Canvas afterCardPlay;
    public bool secondSeven = false;


    public void TransitionPlayerScene()
    {
        if (transitionScreen.enabled == false)
        {
            redTurn.enabled = false;
            greenTurn.enabled = false;
            blueTurn.enabled = false;
            yellowTurn.enabled = false;
            transitionScreen.enabled = true;
            afterCardPlay.enabled = false;
            beforeCardSelect.enabled = true;
            secondSeven = false;
            variant = -1;
            selectedCard = -1;
        }
        else
        {
            transitionScreen.enabled = false;
            switch (turnManager.currentTurn)
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
            UpdateDisplay();
        }
    }

    public Pawn SelectPawn()
    {
        Vector3 position = camera.ScreenToWorldPoint(Input.mousePosition);
        foreach (Pawn pawn in board.GetMoveablePawns(turnManager.currentTurn, turnManager.players[(int)turnManager.currentTurn].playerHand.cards[selectedCard]))
         {
            if (Vector3.Distance(pawn.gameObject.transform.position, position) < 2f/7f)
            {
                return pawn;
            }
        }
        return null;
    }

    public void PlayCard()
    {
        Pawn pawn = SelectPawn();
        if(pawn != null)
        {
            board.MovePawnNumber(pawn, turnManager.players[(int)turnManager.currentTurn].playerHand.cards[selectedCard]);
            if(turnManager.players[(int)turnManager.currentTurn].playerHand.cards[selectedCard].Type == 2)
            {
                turnManager.players[(int)turnManager.currentTurn].playerHand.DiscardCard(selectedCard);
                selectedCard = -1;
                beforeCardSelect.enabled = true;
                afterCardSelect.enabled = false;
                UpdateDisplay();
            }
            else if(!secondSeven && turnManager.players[(int)turnManager.currentTurn].playerHand.cards[selectedCard].Type == 7 && variant != 0)
            {
                secondSeven = true;
            }
            else
            {
                DiscardCard(true);
            }
        }
    }

    public void CancelPlay()
    {
        if (!secondSeven)
        {
            selectedCard = -1;
            variant = -1;
            afterCardSelect.enabled = false;
            beforeCardSelect.enabled = true;
        }
    }

    public void DiscardCard(bool overrideRequirements = false)
    {
        if (overrideRequirements || !turnManager.players[(int)turnManager.currentTurn].playerHand.CheckPlayableCard(turnManager.currentTurn, board))
        {
            turnManager.players[(int)turnManager.currentTurn].playerHand.DiscardCard(selectedCard);
            turnManager.EndTurn();
            afterCardSelect.enabled = false;
            afterCardPlay.enabled = true;
        }
    }

    public void DiscardHand()
    {
        if (!turnManager.players[(int)turnManager.currentTurn].playerHand.CheckPlayableCard(turnManager.currentTurn, board))
        {
            turnManager.players[(int)turnManager.currentTurn].playerHand.DiscardCard(4);
            turnManager.EndTurn();
            afterCardSelect.enabled = false;
            afterCardPlay.enabled = true;
        }
    }

    public void SelectCard(int index)
    {
        if (selectedCard == -1 && index < turnManager.players[(int)turnManager.currentTurn].playerHand.cards.Count)
        {
            selectedCard = index;
            int type = turnManager.players[(int)turnManager.currentTurn].playerHand.cards[selectedCard].Type;
            if (board.GetMoveablePawns(turnManager.currentTurn, turnManager.players[(int)turnManager.currentTurn].playerHand.cards[index]).Count > 0)
            {
                if (type == 10)
                {
                    DisplayVariant("Move ten spaces forwards.", "Move one space backwards.");
                }
                else if (type == 11)
                {
                    DisplayVariant("Move eleven spaces forwards.", "Swap the positions of one of your pieces and another piece.");
                }
                else if (type == 7)
                {
                    DisplaySeven();
                }
            }
            beforeCardSelect.enabled = false;
            afterCardSelect.enabled = true;
        }
    }

    public void DisplayVariant(string optionADescription, string optionBDescription)
    {
        variantDescriptionA.text = optionADescription;
        variantDescriptionB.text = optionBDescription;
        variantSelectionScreen.enabled = true;
        cardSpriteHolderSections[(int)turnManager.currentTurn].enabled = false;
    }

    public void DisplaySeven()
    {
        sevenSelectionScreen.enabled = true;
        cardSpriteHolderSections[(int)turnManager.currentTurn].enabled = false;
    }

    public void SelectVariant(int variantSelected)
    {
        variant = variantSelected;
        variantSelectionScreen.enabled = false;
        sevenSelectionScreen.enabled = false;
        cardSpriteHolderSections[(int)turnManager.currentTurn].enabled = true;
    }

    public void SelectPlayerCount(int count)
    {
        turnManager.playerCount = (byte)count;
        turnManager.EndTurn();
        while (turnManager.currentTurn != E_Color.Red)
        {
            turnManager.EndTurn();
        }
        transitionScreen.enabled = true;
        startMenu.enabled = false;
        UpdateDisplay();
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

        cardDataHolders = new TMP_Text[4, 2, 4];
        for(int i = 0; i < 4; i++)
        {
            cardDataHolders[0, 0, i] = redCardLabels[i];
            cardDataHolders[1, 0, i] = blueCardLabels[i];
            cardDataHolders[2, 0, i] = yellowCardLabels[i];
            cardDataHolders[3, 0, i] = greenCardLabels[i];
            cardDataHolders[0, 1, i] = redCardDescriptions[i];
            cardDataHolders[1, 1, i] = blueCardDescriptions[i];
            cardDataHolders[2, 1, i] = yellowCardDescriptions[i];
            cardDataHolders[3, 1, i] = greenCardDescriptions[i];
        }
    }

    public void DisplayWin()
    {
        string color = "";
        switch (turnManager.currentTurn)
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

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 25 * Time.deltaTime * camera.orthographicSize;
            camera.orthographicSize = Mathf.Max(1, camera.orthographicSize);
        }
    }
    public void UpdateDisplay()
    {
        for(int i = 0; i < 4; i++)
        {
            if(i < turnManager.players[(int)turnManager.currentTurn].playerHand.cards.Count)
            {
                cardDataHolders[(int)turnManager.currentTurn, 0, i].text = turnManager.players[(int)turnManager.currentTurn].playerHand.cards[i].ToString();
                cardDataHolders[(int)turnManager.currentTurn, 1, i].text = turnManager.players[(int)turnManager.currentTurn].playerHand.cards[i].Description;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
