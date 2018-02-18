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

        private void OnMessage(MyMessage message)
        {
            string output = String.Format("{0} - {1} - {2}", message.Name, message.AccountType, message.Content);

            listBoxMessages.Items.Add(output);
        }

        public Form1()
        {
            InitializeComponent();
            client = new Client(OnMessage);

            comboBoxAccountType.Items.AddRange(Enum.GetNames(typeof(AccountType)));
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
            string name = textBoxName.Text;
            AccountType type = (AccountType)Enum.Parse(typeof(AccountType), comboBoxAccountType.SelectedItem.ToString());
            string content = textBoxContent.Text;

            MyMessage myMSG = new MyMessage(content, name, type);

            server.SendNewMessage(myMSG);

            textBoxContent.Clear();
            comboBoxAccountType.SelectedIndex = -1;
        }
    }

    class Client : IClient
    {
        private Action<MyMessage> OnMessage;

        public Client(Action<MyMessage> OnMessage)
        {
            this.OnMessage = OnMessage;
        }

        public void OnNewMessage(MyMessage message)
        {
            OnMessage.Invoke(message);
        }
    }


}
