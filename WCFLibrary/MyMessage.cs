using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace WCFLibrary
{
    [DataContract]
    public enum AccountType
    {
        [EnumMember]
        User,
        [EnumMember]
        Admin,
        [EnumMember]
        Editor
    }

    [DataContract]
    public class MyMessage
    {
        [DataMember]
        public string Content { set; get; }

        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public AccountType AccountType { set; get; }

        public MyMessage(string Content, string Name, AccountType AccountType)
        {
            this.Content = Content;
            this.Name = Name;
            this.AccountType = AccountType;
        }
    }
}
