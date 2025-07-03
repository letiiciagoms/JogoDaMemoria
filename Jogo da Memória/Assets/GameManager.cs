using UnityEngine;


public class GameManager : MonoBehaviour
{
    public PlayerPiece player1;
    public PlayerPiece player2;
    public Dice dice;

    private int currentPlayer = 1;
    private bool waitingForInput = true;

    void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            TakeTurn();
        }
    }

    void TakeTurn()
    {
        waitingForInput = false;

        int roll = dice.Roll();

        if (currentPlayer == 1)
        {
            player1.Move(roll);
            if (player1.HasWon())
            {
                Debug.Log("Jogador 1 venceu!");
                return;
            }
            currentPlayer = 2;
        }
        else
        {
            player2.Move(roll);
            if (player2.HasWon())
            {
                Debug.Log("Jogador 2 venceu!");
                return;
            }
            currentPlayer = 1;
        }

        waitingForInput = true;
    }
}
