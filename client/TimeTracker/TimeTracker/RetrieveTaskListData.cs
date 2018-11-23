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
    class RetrieveTaskListData
    {
        [DataMember]
        string SessionKey;

        private static DataContractJsonSerializer serializerTaskData =
            new DataContractJsonSerializer(typeof(RetrieveTaskListData));

        public System.IO.MemoryStream GetMemoryStream()
        {
            var retrieveTaskStream = new System.IO.MemoryStream();
            serializerTaskData.WriteObject(retrieveTaskStream, this);
            return retrieveTaskStream;
        }
    }
}
