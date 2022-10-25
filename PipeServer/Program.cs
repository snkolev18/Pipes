using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace PipeServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var server = new NamedPipeServerStream("ServerPipe", PipeDirection.InOut, 10);
                Console.WriteLine("Waiting for connections...");
                server.WaitForConnection();

                string username = server.GetImpersonationUserName();
                Console.WriteLine("Connection received");
                Console.WriteLine($"A client has connected: {username}");

                while (true)
                {
                    byte[] buffer = new byte[256];
                    int length = server.Read(buffer, 0, buffer.Length);

                    if(length == 0)
                    {
                        Console.WriteLine("End of stream reached");
                        break;
                    }
                    Console.WriteLine($"Received bytes {length}");

                    string text = Encoding.UTF8.GetString(buffer, 0, length);
                    Console.WriteLine(text);
                }

                server.Disconnect();
            }
            catch (IOException ioe)
            {
                Console.WriteLine($"Exception appeared: {ioe.Message}");
            }
        }
    }
}