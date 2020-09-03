// A C# program for Server 
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{

    public class Program
    {
        public static int Main(string[] args)
        {
            StartServer();
            return 0;

        }

        public static void StartServer()
        {

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);


            try
            {

                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                while(true)
                {
                    Socket handler = listener.Accept();
                    Task.Factory.StartNew(obj => 
                    {
                        string data = null;
                        byte[] bytes = null;

                        while (true)
                        {
                            bytes = new byte[1024];
                            int bytesRec = handler.Receive(bytes);
                            data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            Console.WriteLine("Text received : {0}", data);
                            byte[] msg = Encoding.ASCII.GetBytes(data);
                            handler.Send(msg);



                            if (data.IndexOf("seeya") > -1)
                            {
                                break;
                            }
                        }
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();

                    }, listener);

                }

                

                


                

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
    
