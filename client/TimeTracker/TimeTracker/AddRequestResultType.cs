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
    public sealed class AddRequestResult
    {
        public string AddResult { get; set; }
    
        private static DataContractJsonSerializer deserializerAddRequestResultType =
                new DataContractJsonSerializer(typeof(AddRequestResult));

        public static AddRequestResult ReadFromStream(System.IO.Stream stream)
        {
        return (AddRequestResult)deserializerAddRequestResultType.ReadObject(stream);
        }

        public bool IsSuccess()
        {
        return AddResult.Equals("Success");
        }
}
}
