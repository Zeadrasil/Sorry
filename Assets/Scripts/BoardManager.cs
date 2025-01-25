using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	[SerializeField] public List<Pawn> pawns;
	//[SerializeField] public TurnManager turnManager
	//[SerializeField] public CardManager cardManager
	[SerializeField] public BoardSpace[] starts;

	//public List<Pawn> GetMoveablePawns(Color playerColor, Card usedCard) {}
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
				pawn.location = pawn.location.Traverse(card.Type, pawn.color);
				break;
		}
	}

	public void Slide() {
		
	}

	public void MovePawn7() {
		// 
	}

	public void MovePawn10() {
		
	}

	public void MovePawn11() {
		
	}

	public void MovePawnSorry() {
		
	}
}
