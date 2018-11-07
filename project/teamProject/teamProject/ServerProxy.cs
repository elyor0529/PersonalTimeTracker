using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace teamProject
{
    public  class ServerProxy
    {
        public static ServerProxy instance = null;

        HttpClient httpClient = new HttpClient();
       
        public string host = "localhost";
        public int port = 8000;
        private static readonly object mutex = new object();
        public  ServerProxy Instance
        {
            get
            {
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new ServerProxy();
                    }
                    return instance;
                }
            }
        }

        public  ServerProxy()
        {
            try
            {
                
                Console.WriteLine("Connecting.....");
            }
            catch (SocketException SE)
            {
                string error = "An error occured while connecting [" + SE.Message + "]\n";
            }
        }

        public async Task sendRequest()
        {

  
            //System.IO.MemoryStream stream = new System.IO.MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SessionType));
            var content = await httpClient.GetAsync("http://172.20.10.5:8000/newUnauthorizedSession");
            System.IO.Stream stream = await content.Content.ReadAsStreamAsync();
            Console.WriteLine(await content.Content.ReadAsStringAsync());
            SessionType obj = (SessionType)ser.ReadObject(stream);
            Console.WriteLine(obj.Session);
            Console.WriteLine("Connections done");
            Console.ReadLine();
        }

        public async Task postLogin(Login message)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Login));
            ser.WriteObject(stream, message);
            string a = stream.ToString();
            HttpContent httpContent = new StreamContent(stream);
            var content = await httpClient.PostAsync("http://172.20.10.5:8000/Login", httpContent);

        }
    }
         

}
