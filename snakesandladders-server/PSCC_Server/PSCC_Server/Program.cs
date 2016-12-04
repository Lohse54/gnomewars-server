using System;

namespace PSCC_Server
{


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
    }
