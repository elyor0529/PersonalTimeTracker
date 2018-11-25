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
    public sealed class AddTaskRequest
    {
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public System.Double TimeSpend { get; set; }
        [DataMember]
        public string TaskDate { get; set; }
        [DataMember]
        public string SessionKey { get; set; }
        private static DataContractJsonSerializer serializerAddRequestData =
            new DataContractJsonSerializer(typeof(AddTaskRequest));

        public System.IO.MemoryStream GetMemoryStream()
        {
            var addRequestSendingStream = new System.IO.MemoryStream();
            serializerAddRequestData.WriteObject(addRequestSendingStream, this);
            return addRequestSendingStream;
        }
    }
}
