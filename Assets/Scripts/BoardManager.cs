using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	[SerializeField] public List<Pawn> pawns;
	//[SerializeField] public TurnManager turnManager
	//[SerializeField] public CardManager cardManager
	[SerializeField] public UIManager uiManager;
	[SerializeField] public List<BoardSpace> starts;
	public TurnManager turnManager;
	private Pawn previousPawn;
	[SerializeField] private Pawn TestPawn;

	private void Start()
	{
		//TestPawnDeath();
		//TestPawnCapture();
		foreach (var pawn in pawns)
		{
			PawnDie(pawn);
		}
	}

	private void TestPawnDeath()
	{
		// initialize pawns
		Pawn red = Instantiate(TestPawn.gameObject).GetComponent<Pawn>();
		red.color = E_Color.Red;
		Pawn blue = Instantiate(TestPawn.gameObject).GetComponent<Pawn>();
		blue.color = E_Color.Blue;
		Pawn yellow = Instantiate(TestPawn.gameObject).GetComponent<Pawn>();
		yellow.color = E_Color.Yellow;
		Pawn green = Instantiate(TestPawn.gameObject).GetComponent<Pawn>();
		green.color = E_Color.Green;

		pawns.Add(red);
		pawns.Add(blue);
		pawns.Add(yellow);
		pawns.Add(green);

		// reset pawns intro proper locations
		PawnDie(red);
		PawnDie(blue);
		PawnDie(yellow);
		PawnDie(green);

		// starts are ordered as red, blue, yellow, green
		// Check if each of the different colored pawns were placed into the right spots
		Debug.Log("Is red in its start location? " + ((red.location == starts[0]) ? "Yes" : "No"));
		Debug.Log("Is blue in its start location? " + ((blue.location == starts[1]) ? "Yes" : "No"));
		Debug.Log("Is yellow in its start location? " + ((yellow.location == starts[2]) ? "Yes" : "No"));
		Debug.Log("Is green in its start location? " + ((green.location == starts[3]) ? "Yes" : "No"));

		// cleanup the environment
		pawns.Remove(red);
		pawns.Remove(blue);
		pawns.Remove(yellow);
		pawns.Remove(green);

		Destroy(red.gameObject);
		Destroy(blue.gameObject);
		Destroy(yellow.gameObject);
		Destroy(green.gameObject);
	}

	public void TestPawnCapture()
	{
		// Initialize Pawns
		Pawn red = Instantiate(TestPawn.gameObject).GetComponent<Pawn>();
		red.color = E_Color.Red;
		Pawn blue = Instantiate(TestPawn.gameObject).GetComponent<Pawn>();
		blue.color = E_Color.Blue;

		pawns.Add(red);
		pawns.Add(blue);

		// reset pawns intro proper locations
		PawnDie(red);
		PawnDie(blue);

		// Move pawns so that red is behind blue on the board
		MovePawnNumber(red, 1); // 1 is to move out of the start space
		MovePawnNumber(red, 3);
		MovePawnNumber(blue, 1);
		MovePawnNumber(blue, -11);

		Debug.Log("Is red behind the blue pawn?" + ((red.location.nextSpace == blue.location) ? "Yes" : "No"));

		// move infront of the blue pawn
		MovePawnNumber(red, 2);

		Debug.Log("Is red infront of the blue pawn?" + ((red.location.prevSpace == blue.location) ? "Yes" : "No"));

		// move on the blue pawn, capturing it
		MovePawnNumber(red, -1);

		Debug.Log("Did blue return to it's starting location?" + ((blue.location == starts[1]) ? "Yes" : "No"));

		// Cleanup the environment
		pawns.Remove(red);
		pawns.Remove(blue);

		Destroy(red.gameObject);
		Destroy(blue.gameObject);
	}

	//private void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.Z))
	//	{
	//		MovePawnNumber(pawns[0], new Card(1, ""));
	//		print(GetMoveablePawns(pawns[0].color, new Card(-1, "")).Count);
	//	}
	//	if (Input.GetKeyDown(KeyCode.X))
	//	{
	//		MovePawnNumber(pawns[0], new Card(-4, ""));
	//		print(GetMoveablePawns(pawns[0].color, new Card(-4, "")).Count);
	//	}
	//	if (Input.GetKeyDown(KeyCode.C))
	//	{
	//		MovePawnNumber(pawns[1], new Card(1, ""));
	//		print(GetMoveablePawns(pawns[0].color, new Card(-1, "")).Count);
	//	}
	//	if (Input.GetKeyDown(KeyCode.V))
	//	{
	//		MovePawnNumber(pawns[1], new Card(-4, ""));
	//		print(GetMoveablePawns(pawns[0].color, new Card(-4, "")).Count);
	//	}
	//}

	private int getMovementDistance(int type)
	{
		if(type == 7)
		{
			switch(uiManager.variant)
			{
                case 1:
                    return previousPawn == null ? 6 : 1;
                case 2:
                    return previousPawn == null ? 5 : 2;
                case 3:
                    return previousPawn == null ? 4 : 3;
                case 4:
                    return previousPawn == null ? 3 : 4;
                case 5:
                    return previousPawn == null ? 2 : 5;
                case 6:
                    return previousPawn == null ? 1 : 6;
				default:
					return 7;
            }
		}
		else if (type == 10)
		{
			return uiManager.variant == 0 ? 10 : -1;
		}
		else if(type == 11 & uiManager.variant == 1)
		{
			return 0;
		}
		else if(type == 4)
		{
			return -4;
		}
		return type;
	}

	public List<Pawn> GetMoveablePawns(E_Color playerColor, Card usedCard) {
		List<Pawn> gottenpawns = new List<Pawn>();

		foreach (Pawn pawn in pawns)
		{
			if ((pawn.color == playerColor && usedCard.Type != 0) || (usedCard.Type == 0 && pawn.color != playerColor) || (uiManager.secondEleven))
            {
                int actualMovement = getMovementDistance(usedCard.Type);
                bool canTraverse = actualMovement == 0 ? true : (pawn.location.Traverse(actualMovement, playerColor) != null);

				foreach (Pawn paw in pawns)
				{
					if (actualMovement != 0)
					{
						if (paw.color == pawn.color)
						{
							if (paw.location == pawn.location.Traverse(usedCard.Type, playerColor))
							{
								canTraverse = false;
							}
						}
					}
                }
                if ((previousPawn != null && pawn == previousPawn) || pawn.location.nextSpace == null)
                {
                    canTraverse = false;
                }

                if (canTraverse)
				{
					switch (usedCard.Type)
					{
						case 0:
                            {
                                if (!pawn.location.isHomeSpace && !starts.Contains(pawn.location))
                                {
                                    gottenpawns.Add(pawn);
                                }
                                break;
                            }
                        case 11:
							{
								if (!pawn.location.isHomeSpace && !starts.Contains(pawn.location) && (uiManager.secondEleven || (pawn.color == turnManager.currentTurn) ))
								{
									gottenpawns.Add(pawn);
								}
								break;
							}
						case 1:
							gottenpawns.Add(pawn);
							break;
						case 2:
							gottenpawns.Add(pawn);
							break;
						default:
							if (!starts.Contains(pawn.location))
							{
								gottenpawns.Add(pawn);
							}
							break;
					}
				}
			}
		}

		return gottenpawns;
	}

	public void MovePawnNumber(Pawn pawn, int distance)
    {
        if (pawn.location.prevSpace == null)
        {
            distance = 1;
        }
        BoardSpace space = pawn.location.Traverse(distance, pawn.color);

		if (space == null)
		{
			Debug.LogError("Traverse Method on pawn returned null when moving it");
			return;
		}

		foreach (Pawn p in pawns)
		{
			if (p.location == space && p.color != pawn.color)
			{
				PawnDie(p);
			}
		}
		pawn.location = space;
		if (pawn.location.isSlide > 0 && pawn.color != pawn.location.homeColor)
		{
			Slide(pawn.location.isSlide, pawn);
		}

		pawn.Reposition();
	}

	public void MovePawnNumber(Pawn pawn, Card card, int selection = 0) {
		// switch (card number) {
		//		Cases for each special card
		//		default case for non-special cards that just move the player
		switch (card.Type)
		{
			case 0: // Sorry card
				MovePawnSorry(pawn);
				break;
			case 4:
                MovePawnNumber(pawn, -4);
                break;
            case 7:
				MovePawn7(pawn, selection);
				break;
			case 10:
				MovePawn10(pawn, selection);
				break;
			case 11:
				MovePawn11(pawn, selection);
				break;
			default:
				MovePawnNumber(pawn, card.Type);
				break;
		}
	}

	public bool CheckWin(E_Color color)
	{
		int wincheck = 0;
		foreach (Pawn pawn in pawns)
		{
			if (pawn.color == color && pawn.location.nextSpace == null)
			{
				wincheck++;
			}
		}
		if (wincheck == 4)
		{
			return true;
		}
		return false;
	}

	public void Slide(int distance, Pawn pawn) {
		BoardSpace space = pawn.location;
		for (int i = 1; i < distance; i++)
		{
			space = space.nextSpace;
			foreach (Pawn p in pawns)
			{
				if (p.location == space)
				{
					PawnDie(p);
				}
			}
		}
		pawn.location = space;
	}

	public void PawnDie(Pawn pawn)
	{
        foreach (BoardSpace start in starts)
        {
            if (start.homeColor == pawn.color)
			{
				pawn.location = start;
				pawn.Reposition();
			}
        }
    }

	public void MovePawn7(Pawn pawn, int selection) {
		// move the pawn an amount
		MovePawnNumber(pawn, uiManager.secondSeven ? selection : 7 - selection);
		if (selection == 0 || uiManager.secondEleven)
		{
			previousPawn = null;
		}
		else
        {
            previousPawn = pawn;
        }
	}

	public void MovePawn10(Pawn pawn, int selection) {
		// Move pawn 10 forward or 1 backward (This might not be necessary depending on how we choose between the two options)
		switch (selection)
		{
			case 1:
				MovePawnNumber(pawn, -1);
				break;
			default:
				MovePawnNumber(pawn, 10);
				break;
		}
	}

	public void MovePawn11(Pawn pawn, int selection) {
		// Move a pawn 11 forward or swap places with an enemy pawn
		switch (selection)
		{
			case 1:
				// Get pawn and do a sorry on it with another pieces
				if(uiManager.secondEleven)
				{
					BoardSpace location = previousPawn.location;
					Vector3 position = previousPawn.transform.position;
					previousPawn.location = pawn.location;
					previousPawn.transform.position = pawn.transform.position;
					pawn.location = location;
					pawn.transform.position = position;
                    if (previousPawn.location.isSlide > 0 && previousPawn.color != previousPawn.location.homeColor)
                    {
                        Slide(previousPawn.location.isSlide, previousPawn);
                    }
                    if (pawn.location.isSlide > 0 && pawn.color != pawn.location.homeColor)
                    {
                        Slide(pawn.location.isSlide, pawn);
                    }
					previousPawn = null;
                }
				else
				{
					previousPawn = pawn;
				}
				break;
			default:
				MovePawnNumber(pawn, 11);
				break;
		}
	}

	public void MovePawnSorry(Pawn pawn) {
		// Move a pawn in the start position from start to enemy pawn
		foreach(Pawn movingPawn in pawns)
		{
			if(movingPawn.color == turnManager.currentTurn && movingPawn.location == starts[(int)turnManager.currentTurn])
			{
				movingPawn.location = pawn.location;
				movingPawn.transform.position = pawn.transform.position;
				PawnDie(pawn);
                if (movingPawn.location.isSlide > 0 && movingPawn.color != movingPawn.location.homeColor)
                {
                    Slide(movingPawn.location.isSlide, movingPawn);
                }
				return;
            }
		}
		// set the current pawn location to gotten pawn's location
		// pawn.location = uiManager.getenemypawn(pawn.color).location
	}
}
