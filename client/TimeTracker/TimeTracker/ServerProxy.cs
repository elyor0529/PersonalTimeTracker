using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization.Json;


namespace TimeTracker
{
    public class ServerProxy
    {
        public static ServerProxy instance = null;

        HttpClient httpClient;

        public string host = "127.0.0.1";
        public int port = 8000;
        public string serverURL;
        private static readonly object mutex = new object();
        public ServerProxy Instance
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

        public ServerProxy()
        {
            try
            {
                serverURL = "http://" + host + ":" + port;
                httpClient = new HttpClient();
            }
            catch (SocketException SE)
            {
                string error = "An error occured while connecting [" + SE.Message + "]\n";
            }
        }
        public async Task<SessionType> GetUnauthorizedSession()
        {
            try
            {
                var content = await httpClient.GetAsync(serverURL + "/newUnauthorizedSession");
                System.IO.Stream stream = await content.Content.ReadAsStreamAsync();
                return SessionType.ReadFromStream(stream);
            }
            catch (HttpRequestException)
            {
                throw new Exception("An unhandled exception in ServerProxy occurred.");
            }
        }

        public async Task<LoginResultType> LogIn(LoginData data)
        {
            try
            {
                data.TimeZone = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Today).Hours;
                HttpContent httpContent = new ByteArrayContent(data.GetMemoryStream().ToArray());
                var content = await httpClient.PostAsync(serverURL + "/Login", httpContent);
                return LoginResultType.ReadFromStream(await content.Content.ReadAsStreamAsync());
            }
            catch (HttpRequestException)
            {
                throw new Exception("An unhandled exception in ServerProxy occurred.");
            }
        }
        public async Task<CreateAccountResultType> CreateAccount(CreateAccountData data)
        {
            try
            {
                data.TimeZone = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Today).Hours;
                HttpContent httpContent = new ByteArrayContent(data.GetMemoryStream().ToArray());
                var content = await httpClient.PostAsync(serverURL + "/CreateAccount", httpContent);
                return CreateAccountResultType.ReadFromStream(await content.Content.ReadAsStreamAsync());
            }
            catch (HttpRequestException)
            {
                throw new Exception("An unhandled exception in ServerProxy occurred.");
            }
        }

        public async Task<RetrieveTaskResultType> GetAllTasks(RetrieveTaskListData data)
        {
            try
            {
                HttpContent httpContent = new ByteArrayContent(data.GetMemoryStream().ToArray());
                var content = await httpClient.PostAsync(serverURL + "/GetAllTasks", httpContent);
                return RetrieveTaskResultType.ReadFromStream(await content.Content.ReadAsStreamAsync());
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<SendTaskResultType> SendTask(TaskData data)
        {
            try
            {
                HttpContent httpContent = new ByteArrayContent(data.GetMemoryStream().ToArray());
                var content = await httpClient.PostAsync(serverURL + "/AddTask", httpContent);
                return SendTaskResultType.ReadFromStream(await content.Content.ReadAsStreamAsync());
            }
            catch (HttpRequestException)
            {
                throw new Exception("An unhandled exception in ServerProxy occurred.");
            }
        }
    }
}
