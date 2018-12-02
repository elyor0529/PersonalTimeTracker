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
    public class RecoverPasswordData
    {
        [DataMember]
        public string Email;

        [DataMember]
        public string SessionKey;

        private static DataContractJsonSerializer serializerRecoverPasswordData =
            new DataContractJsonSerializer(typeof(RecoverPasswordData));

        public System.IO.MemoryStream GetMemoryStream()
        {
            var retrieveRecoverPasswordDataStream = new System.IO.MemoryStream();
            serializerRecoverPasswordData.WriteObject(retrieveRecoverPasswordDataStream, this);
            return retrieveRecoverPasswordDataStream;
        }
    }
}
