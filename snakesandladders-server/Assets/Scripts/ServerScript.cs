using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class ServerScript : MonoBehaviour {

    public static void Main()
    {
        NewClient addNewClient = new NewClient();
    }

    //This method will update the new postion of one player to the other plauers
    void UpdatePlacement(Vector2D _playerPos)
    {

        //Will call method from the client and get info about position
        if (Placement())
        {

        }
    }
}

public class NewClient
{
    int clientNumber;
    int port = 1615;
    TcpListener listener;

    public NewClient()
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        addClients();
    }

    public void addClients()
    {
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread newThread = new Thread(new ParameterizedThreadStart(communication));
            newThread.Start(client);
        }
    }

    public void communication(object obj)
    {
        TcpClient client1 = (TcpClient)obj;
        StreamWriter writer = new StreamWriter(client1.GetStream(), Encoding.ASCII) { AutoFlush = true };
        StreamReader reader = new StreamReader(client1.GetStream(), Encoding.ASCII);

        while (true)
        {
            string inputLine = "";
            int count = 0;
            clientNumber += 1;
            Console.WriteLine("Client number: " + clientNumber + " has been initialized");
            while (inputLine != null)
            {
                inputLine = reader.ReadLine();

                foreach (char c in inputLine)
                {
                    if (c == 'a')
                        count++;
                }
                writer.WriteLine("Echoing string: " + inputLine + " , was sent from client number: " + clientNumber + ", Number of a's in the sentence was: " + count);
                Console.WriteLine("Echoing string: " + inputLine + " , was sent from client number: " + clientNumber + ", Number of a's in the sentence was: " + count);
            }
            Console.WriteLine("Server saw disconnect from client.");
        }
    }
}
