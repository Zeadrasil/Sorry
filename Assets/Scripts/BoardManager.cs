using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	[SerializeField] public List<Pawn> pawns;
	//[SerializeField] public TurnManager turnManager
	//[SerializeField] public CardManager cardManager
	[SerializeField] public BoardSpace[] starts;

	//public List<Pawn> GetMoveablePawns(Color playerColor, Card usedCard) {}
	public void MovePawnNumber(int amount) { }
	public void Slide() { }
	public void MovePawn7() { }
	public void MovePawn10() { }
	public void MovePawn11() { }
	public void MovePawnSorry() { }
}
