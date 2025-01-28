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
		try
		{
			BoardSpace toReturn = nextSpace;
			if (distance < 0)
			{
				toReturn = prevSpace;
			}
			else if (goingHome != null && goingHome.homeColor == pawnColor)
			{
				toReturn = goingHome;
			}

			if (distance > 0)
			{
				for (int i = 0; i < distance - 1; i++)
				{
					if (toReturn.goingHome != null && toReturn.homeColor == pawnColor)
					{
						toReturn = toReturn.goingHome;
					}
					else
					{
						toReturn = toReturn.nextSpace;
					}
				}
			}
			else
			{
				for (int i = 0; i > distance + 1 ; i--)
				{
					toReturn = toReturn.prevSpace;
				}
			}

			return toReturn;
		}
		catch
		{
			return null;
		}
	}
}
