using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;


namespace teamProject
{
    class Program
    {
        static void Main(string[] args)
        {



            ServerProxy s = new ServerProxy();
            LoginData j = new LoginData
            {
                UserName = "Jalpesh",
                Email = "Vadgama",
                SessionKey = "999"
            };
            s.sendRequest().Wait();
            s.postLogin(j).Wait();
            /*System.IO.MemoryStream stream = new System.IO.MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Login));
            ser.WriteObject(stream, j);
            byte[] b = stream.ToArray();

            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string jason = sr.ReadToEnd();*/
            
            
            
           
        }
    }
}
