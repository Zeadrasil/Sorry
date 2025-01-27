using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	[SerializeField] public List<Pawn> pawns;
	//[SerializeField] public TurnManager turnManager
	//[SerializeField] public CardManager cardManager
	[SerializeField] public List<BoardSpace> starts;
	public Deck deck;

	private void Start()
	{
		foreach (var pawn in pawns)
		{
			PawnDie(pawn);
		}
	}

	//private void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.Z))
	//	{
	//		MovePawnNumber(pawns[0], new Card(1,""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.X))
	//	{
	//		MovePawnNumber(pawns[0], new Card(-4, ""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.C))
	//	{
	//		MovePawnNumber(pawns[1], new Card(1, ""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.V))
	//	{
	//		MovePawnNumber(pawns[1], new Card(-4, ""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.B))
	//	{
	//		MovePawnNumber(pawns[2], new Card(1, ""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.N))
	//	{
	//		MovePawnNumber(pawns[2], new Card(-4, ""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.M))
	//	{
	//		MovePawnNumber(pawns[3], new Card(1, ""));
	//	}
	//	if (Input.GetKeyDown(KeyCode.Comma))
	//	{
	//		MovePawnNumber(pawns[3], new Card(-4, ""));
	//	}
	//}

	public List<Pawn> GetMoveablePawns(E_Color playerColor, Card usedCard) {
		List<Pawn> gottenpawns = new List<Pawn>();

		foreach (Pawn pawn in pawns)
		{
			if (pawn.color == playerColor)
			{
				bool canTraverse = (pawn.location.Traverse(usedCard.Type, playerColor) != null);

				if (canTraverse)
				{
					switch (usedCard.Type)
					{
						case 0: // sorry card
							if (starts.Contains(pawn.location))
							{
								gottenpawns.Add(pawn);
							}
							break;
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

	public void MovePawnNumber(Pawn pawn, Card card) {
		// switch (card number) {
		//		Cases for each special card
		//		default case for non-special cards that just move the player
		switch (card.Type)
		{
			case 0: // Sorry card
				MovePawnSorry();
				break;
			case 7:
				MovePawn7();
				break;
			case 10:
				MovePawn10();
				break;
			case 11:
				MovePawn11();
				break;
			default:
				BoardSpace space = pawn.location.Traverse(card.Type, pawn.color);

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
				break;
		}
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

	public void MovePawn7() {
		// move the pawn an amount, then ask the UI manager to do the process again with the remaining amount of moves
	}

	public void MovePawn10() {
		// Move pawn 10 forward or 1 backward (This might not be necessary depending on how we choose between the two options)
	}

	public void MovePawn11() {
		// Move a pawn 11 forward or swap places with an enemy pawn
	}

	public void MovePawnSorry() {
		// Move a pawn in the start position from start to enemy pawn
	}
}
