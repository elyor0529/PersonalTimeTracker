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
    public sealed class SendTaskResultType
    {

        private static DataContractJsonSerializer deserializerSendTaskResultType =
            new DataContractJsonSerializer(typeof(SendTaskResultType));

        [DataMember]
        public string AddResult;

        public static SendTaskResultType ReadFromStream(System.IO.Stream stream)
        {
            return (SendTaskResultType)deserializerSendTaskResultType.ReadObject(stream);
        }

        public bool IsSuccess(){
            return AddResult.Equals("Success");
        }
    }
}
