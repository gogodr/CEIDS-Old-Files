using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerClientCEIDS
{
    public class Server
    {
        TcpListener serverSocket;
        TcpClient clientSocket;
        NetworkStream networkStream;
        public bool conectado;
        Thread thread;
        public string paquete;
        public Server(string ipstr, int port)
        {
            IPAddress ip = IPAddress.Parse(ipstr);
            serverSocket = new TcpListener(ip, port);
            clientSocket = default(TcpClient);
            conectado = false;
            paquete = "";
            thread = new Thread(new ThreadStart(escuchar));
        }

        public void comenzarServidor()
        {
            serverSocket.Start();
            Console.WriteLine(" Servidor :: ON ");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" Aceptar conexion con cliente ");
            conectado = true;
        }

        public void nuevaConexion()
        {
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" Aceptar conexion con cliente ");
            conectado = true;
        }

        public void apagarServidor()
        {
            clientSocket.Close();
            serverSocket.Stop();
        }

        public void leer()
        {
            networkStream = clientSocket.GetStream();
            if (networkStream.CanRead == true)
            {
                conectado = true;

                byte[] bytesFrom = new byte[10025];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                paquete = System.Text.Encoding.ASCII.GetString(bytesFrom);
                paquete = paquete.Substring(0, paquete.IndexOf("\0"));
                
                if (paquete == "Comando::Cerrar")
                {
                    conectado = false;

                }

            }
            else
            {

                Console.WriteLine("No se puede leer");
            }
            networkStream.Flush();
        }

        public void enviar(string paquete)
        {
            networkStream = clientSocket.GetStream();
            if (networkStream.CanRead == true)
            {
                conectado = true;

                Byte[] sendBytes = Encoding.ASCII.GetBytes(paquete);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                            }
            else
            {
                Console.WriteLine("No se puede Escribir");
            }
            networkStream.Flush();
        }

        public void comenzarEscuchar() {
            thread.Start();
        }

        private void escuchar() {
            while (conectado) {
                leer();
            }
            nuevaConexion();
            thread.Abort();
        }

    }
}
