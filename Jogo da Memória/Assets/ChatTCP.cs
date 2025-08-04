using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro; 

public class ChatTCP : MonoBehaviour
{
    public TMP_InputField inputMensagem; 
   public TMP_Text chatArea;

    // Fila de mensagens recebidas
    private readonly Queue<string> mensagensRecebidas = new Queue<string>();

    void Start()
    {
        // Iniciar thread para escutar mensagens
        Thread t = new Thread(ReceberMensagens);
        t.IsBackground = true;
        t.Start();
    }

    void Update()
    {
        // Mostrar mensagens recebidas na thread principal
        while (mensagensRecebidas.Count > 0)
        {
            string msg = mensagensRecebidas.Dequeue();
            chatArea.text += msg + "\n";
        }
    }

    public void EnviarMensagem()
    {
        if (string.IsNullOrEmpty(inputMensagem.text)) return;

        string msg = inputMensagem.text;
        byte[] dados = Encoding.UTF8.GetBytes(msg);
        GerenciadorRedeTCP.stream?.Write(dados, 0, dados.Length);

        chatArea.text += "[Você]: " + msg + "\n";
        inputMensagem.text = "";
    }

    void ReceberMensagens()
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            try
            {
                int bytesLidos = GerenciadorRedeTCP.stream.Read(buffer, 0, buffer.Length);
                if (bytesLidos == 0) continue;
                string msg = Encoding.UTF8.GetString(buffer, 0, bytesLidos);
                mensagensRecebidas.Enqueue("[Outro]: " + msg);
            }
            catch
            {
                break;
            }
        }
    }
}