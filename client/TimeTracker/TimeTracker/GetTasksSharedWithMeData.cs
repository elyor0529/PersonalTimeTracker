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
    public class GetTasksSharedWithMeData
    {
        [DataMember]
        public string SessionKey;

        private static DataContractJsonSerializer serializerCreateAccountData =
           new DataContractJsonSerializer(typeof(GetTasksSharedWithMeData));
        public System.IO.MemoryStream GetMemoryStream()
        {
            var createAccountSendingStream = new System.IO.MemoryStream();
            serializerCreateAccountData.WriteObject(createAccountSendingStream, this);
            return createAccountSendingStream;
        }
    }
}
