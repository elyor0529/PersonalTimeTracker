using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Globalization;
using System.IO;

namespace TimeTracker
{
    [DataContract]
    public sealed class GetTaskNameSuggestionResultType
    {
        [DataMember]
        public string GetTaskNameSuggestionResult;
        [DataMember]
        public string[] TaskNames;

        private static DataContractJsonSerializer deserializerRetrieveTaskResultType =
                new DataContractJsonSerializer(typeof(GetTaskNameSuggestionResultType));
        public static GetTaskNameSuggestionResultType ReadFromStream(System.IO.Stream stream)
        {
            GetTaskNameSuggestionResultType result = (GetTaskNameSuggestionResultType)deserializerRetrieveTaskResultType.ReadObject(stream);
            return result;
        }

        public bool IsSuccess()
        {
            return GetTaskNameSuggestionResult.Equals("Success");
        }
    }
}
    