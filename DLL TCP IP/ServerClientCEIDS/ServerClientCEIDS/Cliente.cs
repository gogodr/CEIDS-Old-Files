using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerClientCEIDS
{
    public class Cliente
    {
        TcpClient socket;
        NetworkStream serverStream;
        
        public Cliente()
        {
            socket = new System.Net.Sockets.TcpClient();

        }

        public void conectar(string ip, int puerto)
        {
            socket.Connect(ip, puerto);
            serverStream = socket.GetStream();
        }

        public void close()
        {
            Thread.Sleep(10);
            enviar("Comando::Cerrar\0");
            serverStream.Close();
            socket.Close();
        }

        public void enviar(string paquete)
        {
            if (serverStream.CanWrite == true)
            {
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(paquete);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            else
            {
                Console.WriteLine("No se puede escribir");
            }
        }

        public void recibir(ref string paquete)
        {
            if (serverStream.CanRead == true)
            {
                byte[] inStream = new byte[10025];
                serverStream.Read(inStream, 0, (int)socket.ReceiveBufferSize);
                paquete = System.Text.Encoding.ASCII.GetString(inStream);
                paquete = paquete.Substring(0, paquete.IndexOf("\0"));

            }
            else
            {
                Console.WriteLine("No se puede leer");
            }
        }

    }
}
