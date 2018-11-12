using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace timetracker
{
    [DataContract]
    public sealed class CreateAccountResultType
    {
        private static DataContractJsonSerializer deserializerLoginResultType =
            new DataContractJsonSerializer(typeof(CreateAccountResultType));

        [DataMember]
        public string CreateAccountResult;
        [DataMember]
        public string SessionKey;


        public static CreateAccountResultType ReadFromStream(System.IO.Stream stream)
        {
            return (CreateAccountResultType)deserializerLoginResultType.ReadObject(stream);
        }
    }
}
