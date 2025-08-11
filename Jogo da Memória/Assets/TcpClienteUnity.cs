using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TcpClientUnity : MonoBehaviour
{
    public InputField input;    // ou TMP_InputField se usar TextMeshPro
    public Button sendButton;
    public string serverIP = "192.168.88.247";

    public int port = 8080;

    void Start()
    {
        if (sendButton == null)
        {
            Debug.LogError("[Cliente] sendButton não atribuído! Arraste o Button no Inspector.");
            return;
        }
        if (input == null)
        {
            Debug.LogWarning("[Cliente] input não atribuído! Arraste o InputField no Inspector.");
        }

        sendButton.onClick.AddListener(SendMessageToServer);
    }

    void SendMessageToServer()
    {
        if (input == null) return;

        string mensagem = input.text;
        if (string.IsNullOrWhiteSpace(mensagem)) return;

        try
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(serverIP, port);
                using (NetworkStream stream = client.GetStream())
                {
                    // Envia
                    byte[] data = Encoding.UTF8.GetBytes(mensagem);
                    stream.Write(data, 0, data.Length);

                    // Recebe resposta (bloqueia até receber)
                    byte[] buffer = new byte[1024];
                    int len = stream.Read(buffer, 0, buffer.Length);
                    if (len > 0)
                    {
                        string resposta = Encoding.UTF8.GetString(buffer, 0, len);
                        Debug.Log($"[Cliente] Resposta do servidor: {resposta}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[Cliente] Erro: " + ex);
        }
    }
}

