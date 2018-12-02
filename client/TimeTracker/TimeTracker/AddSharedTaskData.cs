using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeTracker
{
    [DataContract]
    public class AddSharedTaskData
    {
        [DataMember]
        public string TaskName;


        [DataMember]
        public string TaskDate;


        private DateTime taskDateTime;
        public DateTime TaskDateTime {
        get{
                return taskDateTime;
        }
        set {
                TaskDate = XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc);
                taskDateTime = value;
            }
        }



        [DataMember]
        public double TaskTimeSpent;

        [DataMember]
        public string EmailTo;

        [DataMember]
        public string SessionKey;

        private static DataContractJsonSerializer serializerAddSharedTaskData =
           new DataContractJsonSerializer(typeof(AddSharedTaskData));
        public System.IO.MemoryStream GetMemoryStream()
        {
            var addSharedTaskDataSendingStream = new System.IO.MemoryStream();
            serializerAddSharedTaskData.WriteObject(addSharedTaskDataSendingStream, this);
            return addSharedTaskDataSendingStream;
        }
    }
}
