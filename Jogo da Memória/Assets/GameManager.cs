using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerPiece player1;
    public PlayerPiece player2;
    public Dice dice;

    public TMP_Text textoVez;
    public TMP_Text textoVitoria;

    private int currentPlayer = 1;
    private bool waitingForInput = true;
    private bool gameOver = false;

    void Start()
    {
        AtualizarTextoVez();
    }

    void Update()
    {
        if (gameOver) return;

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
                MostrarVitoria("JOGADOR 1 VENCEU!");
                return;
            }
            currentPlayer = 2;
        }
        else
        {
            player2.Move(roll);
            if (player2.HasWon())
            {
                MostrarVitoria("JOGADOR 2 VENCEU!");
                return;
            }
            currentPlayer = 1;
        }

        AtualizarTextoVez();
        waitingForInput = true;
    }

    void AtualizarTextoVez()
    {
        textoVez.text = $"Vez do Jogador {currentPlayer}";
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void MostrarVitoria(string mensagem)
    {
        Debug.Log("Vitória detectada: " + mensagem);
        textoVitoria.text = mensagem;
        textoVez.text = "";
        gameOver = true;
    }
}
