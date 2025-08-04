using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;

public class GerenciadorRedeTCP : MonoBehaviour
{
    public static TcpClient cliente;
    public static TcpListener servidor;
    public static NetworkStream stream;
    public static bool isHost = false;

    public void IniciarComoHost()
    {
        isHost = true;
        Thread t = new Thread(() =>
        {
            servidor = new TcpListener(IPAddress.Any, 7777);
            servidor.Start();
            Debug.Log("Aguardando cliente...");
            cliente = servidor.AcceptTcpClient();
            stream = cliente.GetStream();
            Debug.Log("Cliente conectado!");
        });
        t.IsBackground = true;
        t.Start();
    }

    public void IniciarComoCliente(string ip)
    {
        isHost = false;
        Thread t = new Thread(() =>
        {
            cliente = new TcpClient();
            cliente.Connect(ip, 7777);
            stream = cliente.GetStream();
            Debug.Log("Conectado ao servidor!");
        });
        t.IsBackground = true;
        t.Start();
    }

    private void OnApplicationQuit()
    {
        stream?.Close();
        cliente?.Close();
        servidor?.Stop();
    }
}