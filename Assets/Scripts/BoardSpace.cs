using UnityEngine;

public class BoardSpace : MonoBehaviour
{
	[SerializeField] public BoardSpace nextSpace;
	[SerializeField] public BoardSpace prevSpace;
	[SerializeField] public BoardSpace goingHome;
	[SerializeField] public E_Color homeColor;
	[SerializeField] public bool isHomeSpace;
	[SerializeField] public bool isSlide;
}
