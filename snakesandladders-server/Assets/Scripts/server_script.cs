using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class server_script : MonoBehaviour {

    //Declaring variables
    Thread receiveThread;

    UdpClient listener;

    int port;


    public static void Main()
    {
        //initializing the script
        server_script receive = new server_script();
        receive.initialize();

        string text = "";
        while (true)
        {
            text = Console.ReadLine();
        }
    }

	void Start ()
    {
        initialize();
    }
	
	void Update ()
    {
	
	}
<<<<<<< HEAD
    //New test
    //Ellisa edit for test
=======

    //Ellis edit for test
    //Ellisa edit for new test 
>>>>>>> origin/master

    void initialize()
    {
        //Setting port
        port = 1234;

        //Instantiating a new thread and runs it
        receiveThread = new Thread(new ThreadStart(data));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    //Receives the thread
    void data()
    {
        //Receives data from new client
        listener = new UdpClient(port);
        while (true)
        {

            try
            {
                //Receives from any IPAdress
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = listener.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);

            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }
}
