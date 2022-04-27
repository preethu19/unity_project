using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UDPTest();
    }

    void UDPTest()
    {

        //Client uses as receive udp client
        UdpClient Client = new UdpClient(5600);

        //CallBack
        void recv(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);
            string receivedString = Encoding.ASCII.GetString(received);
            //Process codes
            print(receivedString);
            byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the client");
            Client.Send(sendBytes, sendBytes.Length);
            Client.BeginReceive(new AsyncCallback(recv), null);
        }

        try
        {
            Client.Connect("127.0.0.1", 5500);
            byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the client");
            Client.Send(sendBytes, sendBytes.Length);
            Client.BeginReceive(new AsyncCallback(recv), null);
        }
        catch (Exception e)
        {
            print("Exception thrown" + e.Message);
        }
    }


    }


