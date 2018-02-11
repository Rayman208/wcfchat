using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using WCFLibrary;
using System.ServiceModel.Channels;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            ServiceHost host = new ServiceHost(server);
            host.Open();
            Console.WriteLine("Сервер запущен (для остановки нажмите любую клавищу)");
            Console.ReadKey();
            host.Close();
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class Server : IServer
    {
        List<IClient> clients = new List<IClient>();
        
        public void Connect()
        {
            IClient client =  OperationContext.Current.GetCallbackChannel<IClient>();
            clients.Add(client);

            MessageProperties messProp = OperationContext.Current.IncomingMessageProperties;
            RemoteEndpointMessageProperty endPoint = messProp[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            string ip = endPoint.Address;
            string port = endPoint.Port.ToString();

            Console.WriteLine("Client {0}:{1} connected",ip,port);
        }

        public void Disconnect()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            clients.Remove(client);

            MessageProperties messProp = OperationContext.Current.IncomingMessageProperties;
            RemoteEndpointMessageProperty endPoint = messProp[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            string ip = endPoint.Address;
            string port = endPoint.Port.ToString();

            Console.WriteLine("Client {0}:{1} disconnected", ip, port);
        }

        public void SendNewMessage(string message)
        {
            foreach (IClient client in clients)
            {
                client.OnNewMessage(message);
            }

            MessageProperties messProp = OperationContext.Current.IncomingMessageProperties;
            RemoteEndpointMessageProperty endPoint = messProp[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            string ip = endPoint.Address;
            string port = endPoint.Port.ToString();

            Console.WriteLine("New message from {0}:{1} = {2}", ip, port, message);
        }
    }
}
