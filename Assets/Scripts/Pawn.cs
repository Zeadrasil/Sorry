using UnityEngine;

public class Pawn : MonoBehaviour
{
    public BoardSpace location;
    public E_Color color;

    public void Reposition()
    {
        // Positions the pawn into its location
        transform.position = location.transform.position;
    }
}
