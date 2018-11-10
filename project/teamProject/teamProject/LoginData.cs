using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.Runtime.Serialization.Json;

namespace teamProject
{
    [DataContract]
    public sealed class LoginData
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string SessionKey { get; set; }

        private static DataContractJsonSerializer serializerLoginData =
            new DataContractJsonSerializer(typeof(LoginData));

        public System.IO.MemoryStream GetMemoryStream(){
            var loginSendingStream = new System.IO.MemoryStream();
            serializerLoginData.WriteObject(loginSendingStream, this);
            return loginSendingStream;
        }
    }
}
