using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text;

namespace PipeClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new NamedPipeClientStream(".", "ServerPipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);

            Console.WriteLine("Connecting to server...");
            client.Connect();

            while (true)
            {
                Console.WriteLine("Enter text: ");
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine());

                client.Write(bytes, 0, bytes.Length);
            }

            

        }
    }
}