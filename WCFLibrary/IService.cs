using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace WCFLibrary
{
    [ServiceContract(CallbackContract = typeof(IClient),SessionMode = SessionMode.Required)]
    public interface IServer
    {
        [OperationContract(IsInitiating = true)]
        void Connect();

        [OperationContract(IsTerminating = true)]
        void Disconnect();

        [OperationContract(IsOneWay = true)]
        void SendNewMessage(string message);
    }

    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void OnNewMessage(string message);
    }
}
