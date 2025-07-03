using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] faces; // 6 sprites do dado, do 1 ao 6
    public SpriteRenderer diceRenderer; // Referência ao SpriteRenderer

    public int Roll()
    {
        int result = Random.Range(1, 7); // Valor de 1 a 6
        Debug.Log("Dado rolado: " + result);

        // Mostrar a imagem correspondente
        if (diceRenderer != null && faces != null && faces.Length >= 6)
        {
            diceRenderer.sprite = faces[result - 1]; // -1 pois o array começa no 0
        }

        return result;
    }
}
