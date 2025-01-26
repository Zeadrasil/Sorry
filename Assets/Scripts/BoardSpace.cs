using UnityEngine;

public class BoardSpace : MonoBehaviour
{
	[SerializeField] public BoardSpace nextSpace;
	[SerializeField] public BoardSpace prevSpace;
	[SerializeField] public BoardSpace goingHome;
	[SerializeField] public E_Color homeColor;
	[SerializeField] public bool isHomeSpace;
	[SerializeField] public int isSlide;

	public BoardSpace Traverse(int distance, E_Color pawnColor)
	{
		BoardSpace toReturn = nextSpace;
		if (goingHome != null)
		{
			toReturn = goingHome;
		}

		try
		{
			for (int i = 0; i < Mathf.Abs(distance) - 1; i++)
			{
				if (distance > 0)
				{
					if (toReturn.goingHome != null)
					{
						toReturn = toReturn.goingHome;
					}
					else
					{
						toReturn = toReturn.nextSpace;
					}
				}
				else
				{
					toReturn = toReturn.prevSpace;
				}
				
			}
		}
		catch
		{
			return null;
		}

		return toReturn;
	}
}
