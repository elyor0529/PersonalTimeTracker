using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using System.Text;
using System.Threading.Tasks;

namespace TimeTracker
{
    [DataContract]
    public class GetTasksSharedWithMeResult
    {
        [DataMember]
        public string GetAllSharedTasksResult;
        [DataMember]
        public SharedTask[] SharedTaskList;

        private static DataContractJsonSerializer deserializerRetrieveTaskResultType =
                new DataContractJsonSerializer(typeof(GetTasksSharedWithMeResult));
        public static GetTasksSharedWithMeResult ReadFromStream(System.IO.Stream stream)
        {
            GetTasksSharedWithMeResult result = (GetTasksSharedWithMeResult)deserializerRetrieveTaskResultType.ReadObject(stream);
            for (int i = 0; i < result.SharedTaskList.Length; i++)
            {
                result.SharedTaskList[i].parseDate();
            }
            return result;
        }
    }

    [DataContract]
    public class SharedTask
    {
        [DataMember]
        public string TaskName;

        [DataMember]
        public double TimeSpent;

        [DataMember]
        public string TaskDate;

        [DataMember]
        public string EmailFrom;

        public DateTime TaskDateTime;

        public void parseDate()
        {
            TaskDateTime = DateTime.ParseExact(TaskDate, "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF'Z'", CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
        }

        public DateTime getDateTime()
        {
            return TaskDateTime;
        }

        public override string ToString()
        {
            return TaskName;
        }
    }
}
