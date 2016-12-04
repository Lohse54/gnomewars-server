using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace PSCC_Server
{
    public class TcpEchoServer
    {
        // initialising the class that instantiates a new client
        public static void Main()
        {
            Console.WriteLine("Starting echo server...");
            newClient moreClients = new newClient();
        }
    }


    //Class newClient
    public class newClient
    {
        bool active, active1, yes = true;
        List<TcpClient> listOfClients = new List<TcpClient>();
        string[] players = { "Player1", "Player2", "Player3" };
        int playerSelect = -1, playerNum = 0;
        int port = 1615;
        TcpListener listener;


        //Listening to new client
        public newClient()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            addClients();
        }


        //Accepts a new client and starts it on a thread
        public void addClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                listOfClients.Add(client);
                Thread newThread = new Thread(new ParameterizedThreadStart(communication));
                newThread.Start(client);
            }
        }

        public void communication(object obj)
        {
            // Writer and Reader assigned
            TcpClient client1 = (TcpClient)obj;
            StreamWriter writer = new StreamWriter(client1.GetStream(), Encoding.ASCII) { AutoFlush = true };
            StreamReader reader = new StreamReader(client1.GetStream(), Encoding.ASCII);


            while (true)
            {
                //Assigns name to the client that has been accepted
                string inputLine = "", placements = "";
                playerSelect += 1;
                if (playerSelect > 2)
                {
                    playerSelect = 0;
                }
                Console.WriteLine("Client : " + players[playerSelect] + " has been initialized");
                writer.WriteLine(players[playerSelect]);
                inputLine = reader.ReadLine();
                writer.WriteLine(players[playerSelect]);
                //Loop that wait until all clients have been accepted
                while (yes)
                {
                    inputLine = reader.ReadLine();
                    writer.WriteLine(players[playerSelect]);
                    if (playerSelect > 1)
                    {
                        yes = false;
                    }
                }
                playerNum = 0;
                active = true;
                active1 = true;

                //Loops until game is over
                while (inputLine != null)
                {
                    //Writes to clients until gameover statement has been initialized
                    if (playerNum < 3)
                    {
                        writer.WriteLine(players[playerNum]);
                    }

                    inputLine = reader.ReadLine();


                    // Checks if the client sends its own name to the server and changes, so that the turn is being passed to the next player
                    if (inputLine == players[0] && active == true && active1 == true)
                    {
                        placements = reader.ReadLine();
                        writer.WriteLine(placements);
                        Console.WriteLine(placements + " placements");
                        playerNum = 1;
                        active = false;
                        active1 = true;
                        Console.WriteLine("Player2 turn");
                    }
                    if (inputLine == players[1] && active == false && active1 == true)
                    {
                        playerNum = 2;
                        active = true;
                        active1 = false;
                        Console.WriteLine("Player3 turn");
                    }
                    if (inputLine == players[2] && active == true && active1 == false)
                    {
                        playerNum = 0;
                        active = true;
                        active1 = true;
                        Console.WriteLine("Player1 turn");
                    }

                    //If inputline is equal to statement a winning condition will be sent to clients
                    if (inputLine == "Player1 Won")
                    {
                        playerNum = 3;
                    }
                    if (inputLine == "Player2 Won")
                    {
                        playerNum = 3;
                    }
                    if (inputLine == "Player3 Won")
                    {
                        playerNum = 3;
                    }
                    if (playerNum == 3)
                    {
                        placements = reader.ReadLine();
                        writer.WriteLine(inputLine);
                    }
                }

                Console.WriteLine("Server saw disconnect from client.");
            }
        }
    }
}


