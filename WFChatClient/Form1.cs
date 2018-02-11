using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceModel;
using WCFLibrary;


namespace WFChatClient
{
    public partial class Form1 : Form
    {
        IClient client;
        IServer server;

        private void OnMessage(string message)
        {
            listBoxMessages.Items.Add(message);
        }

        public Form1()
        {
            InitializeComponent();
            client = new Client(OnMessage);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            DuplexChannelFactory<IServer> factory = new DuplexChannelFactory<IServer>(client, "clientendpoint");
            server = factory.CreateChannel();
            server.Connect();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            server.Disconnect();
            (server as ICommunicationObject).Close();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            server.SendNewMessage(textBoxMessage.Text);
            textBoxMessage.Clear();
        }
    }

    class Client : IClient
    {
        private Action<string> OnMessage;

        public Client(Action<string> OnMessage)
        {
            this.OnMessage = OnMessage;
        }

        public void OnNewMessage(string message)
        {
            OnMessage.Invoke(message);
        }
    }


}
