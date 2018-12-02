using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.IO;

namespace TimeTracker
{
    [DataContract]
    public sealed class TaskData {

        [DataMember]    
        public string TaskName;

        [DataMember]
        public float TimeSpent;

        [DataMember]
        public string TaskDate = null;

        [DataMember]
        public string SessionKey;

        private DateTime taskDateTime;
        public DateTime TaskDateTime
        {
            get
            {
                return taskDateTime;
            }
            set
            {
                taskDateTime = value.ToUniversalTime();
                TaskDate = XmlConvert.ToString(taskDateTime, XmlDateTimeSerializationMode.Utc);
            }
        } 

        private static DataContractJsonSerializer serializerTaskData =
            new DataContractJsonSerializer(typeof(TaskData));

        public System.IO.MemoryStream GetMemoryStream()
        {
            if (TaskDate == null)
            {
                DateTime temp = TaskDateTime.ToUniversalTime();
                TaskDate = XmlConvert.ToString(temp, XmlDateTimeSerializationMode.Utc);
            }
            var taskSendingStream = new System.IO.MemoryStream();
            serializerTaskData.WriteObject(taskSendingStream, this);
            return taskSendingStream;
        }
    }
}
