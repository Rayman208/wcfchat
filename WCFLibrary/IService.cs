using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace WCFLibrary
{
    [ServiceContract(CallbackContract = typeof(IClient),SessionMode = SessionMode.Required)]
    [ServiceKnownType(typeof(MyMessage))]
    [ServiceKnownType(typeof(AccountType))]
    public interface IServer
    {
        [OperationContract(IsInitiating = true)]
        void Connect();

        [OperationContract(IsTerminating = true)]
        void Disconnect();

        [OperationContract(IsOneWay = true)]
        void SendNewMessage(MyMessage message);
    }

    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void OnNewMessage(MyMessage message);
    }
}
