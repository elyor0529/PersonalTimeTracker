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
    public class RecoverPasswordResultType
    {

        [DataMember]
        public string UpdatePasswordResult;

        [DataMember]
        public string SessionKey;

        private static DataContractJsonSerializer deserializerRecoverPasswordResultType =
            new DataContractJsonSerializer(typeof(RecoverPasswordResultType));

        public static RecoverPasswordResultType ReadFromStream(System.IO.Stream stream)
        {
            return (RecoverPasswordResultType)deserializerRecoverPasswordResultType.ReadObject(stream);
        }
    }
}
