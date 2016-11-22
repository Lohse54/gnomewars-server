using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class server_script : MonoBehaviour {

    Thread receiveThread;

    UdpClient listener;

    int port;


    public static void Main()
    {
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

    void initialize()
    {
        port = 1234;

        receiveThread = new Thread(new ThreadStart(data));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void data()
    {
        listener = new UdpClient(port);
        while (true)
        {

            try
            {
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
