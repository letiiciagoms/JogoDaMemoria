using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public int currentPosition = 0;
    public Transform[] boardSpaces;

    public void Move(int steps)
    {
        currentPosition += steps;
        if (currentPosition >= boardSpaces.Length)
            currentPosition = boardSpaces.Length - 1;

        transform.position = boardSpaces[currentPosition].position;
    }

    public bool HasWon()
    {
        return currentPosition >= boardSpaces.Length - 1;
    }
}
