using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.Runtime.Serialization.Json;

namespace timetracker
{
    public sealed class CreateAccount
    {
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
        public int TimeZone { get; set;}
        [DataMember]
        public string SessionKey { get; set; }
        private static DataContractJsonSerializer serializerLoginData =
            new DataContractJsonSerializer(typeof(CreateAccount));

        public System.IO.MemoryStream GetMemoryStream()
        {
            var loginSendingStream = new System.IO.MemoryStream();
            serializerLoginData.WriteObject(loginSendingStream, this);
            return loginSendingStream;
        }
    }
}
