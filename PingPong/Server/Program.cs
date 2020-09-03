// A C# program for Client 
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{

    public class Program
    {

        public static int Main(string[] args)
        {
            StartClient();
            return 0;

        }

        public static void StartClient()
        {
            byte[] bytes = new byte[1024];

            try
            {

                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                Socket sender = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                    while (true)
                    {
                        Console.WriteLine("start talking");
                        var a = Console.ReadLine();
                        
                        byte[] msg = Encoding.ASCII.GetBytes(a);
                        int bytesSent = sender.Send(msg);
                        int bytesRec = sender.Receive(bytes);
                        if (a == "seeya")
                        {
                            break;
                        }
                        Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        

                    }
                        //Console.ReadLine();



                        //byte[] msg1 = Encoding.ASCII.GetBytes("hi<EOF>");
                        //int bytesSent1 = sender.Send(msg1);
                        //int bytesRec1 = sender.Receive(bytes);
                        //Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                    
                    



                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
               // Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}