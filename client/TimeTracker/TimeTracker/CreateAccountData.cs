using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TimeTracker
{
    [DataContract]
    public sealed class CreateAccountData

    {
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int TimeZone { get; set; }

        [DataMember]
        public string SessionKey { get; set; }
        private static DataContractJsonSerializer serializerCreateAccountData =
           new DataContractJsonSerializer(typeof(CreateAccountData));
        public System.IO.MemoryStream GetMemoryStream()
        {
            var createAccountSendingStream = new System.IO.MemoryStream();
            serializerCreateAccountData.WriteObject(createAccountSendingStream, this);
            return createAccountSendingStream;
        }

    }
}
