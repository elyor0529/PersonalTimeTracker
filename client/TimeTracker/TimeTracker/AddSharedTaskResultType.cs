using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker
{
    [DataContract]
    public class AddSharedTaskResultType
    {
        [DataMember]
        public string AddSharedTaskResult;

        [DataMember]
        public string SessionKey;

        private static DataContractJsonSerializer deserializerAddRequestResultType =
                    new DataContractJsonSerializer(typeof(AddSharedTaskResultType));

        public static AddSharedTaskResultType ReadFromStream(System.IO.Stream stream)
        {
            return (AddSharedTaskResultType)deserializerAddRequestResultType.ReadObject(stream);
        }
    }
}
