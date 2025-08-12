using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TcpClientUnity : MonoBehaviour
{
    public InputField input;
    public Button sendButton;

    void Start()
    {
        sendButton.onClick.AddListener(SendMessageToServer);
    }

    void SendMessageToServer()
    {
        string mensagem = input.text;
        if (string.IsNullOrWhiteSpace(mensagem)) return;

        try
        {
            TcpClient client = new TcpClient("10.57.10.32", 8080); // Conecta ao servidor
            NetworkStream stream = client.GetStream();

            // Envia mensagem
            byte[] data = Encoding.UTF8.GetBytes(mensagem);
            stream.Write(data, 0, data.Length);

            // Aguarda resposta do servidor
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string resposta = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log($"[Cliente] Resposta do servidor: {resposta}");

            // Fecha conexão
            stream.Close();
            client.Close();
        }
        catch (SocketException ex)
        {
            Debug.LogError($"[Cliente] Erro de conexão: {ex.Message}");
        }
    }
}