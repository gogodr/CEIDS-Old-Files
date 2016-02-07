using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alchemy;
using Alchemy.Classes;
using System.Net;
using System.Collections;
using Newtonsoft.Json;
namespace WebSockets
{
    public partial class Form1 : Form
    {
        public WebSocketServer aServer;
        public ArrayList users;

        public Form1()
        {
            InitializeComponent();
            aServer = new WebSocketServer(81, IPAddress.Any)
            {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnected = OnConnect,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };
            users = new ArrayList();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            AddData("Server Start");
            aServer.Start();          

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            aServer.Stop();
            AddData("Server Stop");
        }

        public void OnConnect(UserContext context)
        {
            AddData("Client Connection From : " + context.ClientAddress);
            users.Add(new User(users.Count, context));

        }
        public void OnReceive(UserContext context)
        {
            AddData("Received Data From :" + context.ClientAddress);
            AddData("Data:" + context.DataFrame.ToString());

        }

        private void AddData(string text)
        {
            if (listData.InvokeRequired)
                listData.Invoke(new Action(() => AddData(text)));
            else
                listData.Items.Add(text);
        }

        public void OnSend(UserContext context)
        {
            AddData("Data Send To : " + context.ClientAddress);
        }

        public void OnDisconnect(UserContext context)
        {
            AddData("Client Disconnected : " + context.ClientAddress);
           
        }

        private void sendbtn_Click(object sender, EventArgs e)
        {
            foreach(User u in users){
                u.context.Send("Holiwi");
            }
        }
    }
}
