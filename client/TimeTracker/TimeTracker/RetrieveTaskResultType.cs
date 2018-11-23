using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace TimeTracker
{
    [DataContract]
    public sealed class RetrieveTaskResultType
    {
        [DataMember]
        public string RetrieveTaskListResult;
        [DataMember]
        public DownloadedTaskType[] TaskList;

        private static DataContractJsonSerializer deserializerRetrieveTaskResultType =
                new DataContractJsonSerializer(typeof(RetrieveTaskResultType));
        public static RetrieveTaskResultType ReadFromStream(System.IO.Stream stream)
        {
            RetrieveTaskResultType result = (RetrieveTaskResultType)deserializerRetrieveTaskResultType.ReadObject(stream);
            DownloadedTaskType task = null ;
            for (int i = 0; i < result.TaskList.Length; i++) {
                task.parseDate();
            }
            return result;
        }

        public bool IsSuccess(){
            return RetrieveTaskListResult.Equals("Success");
        }
    }
    [DataContract]
    public sealed class DownloadedTaskType {
        [DataMember]
        public string TaskName;
        [DataMember]
        public float TimeSpent;
        [DataMember]
        public string TaskDate;

        private DateTime TaskDateTime;

        public void parseDate(){
            TaskDateTime = XmlConvert.ToDateTime(TaskDate, XmlDateTimeSerializationMode.Utc);
        }

        public DateTime getDateTime(){
            return TaskDateTime;
        }
    }
}
