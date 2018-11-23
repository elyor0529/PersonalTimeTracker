using System;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace TimeTracker
{
    class ServerProxyTest
    {
        const string serverIP = "127.0.0.1";
        const string serverPort = "8000";
        ServerProxy serverProxy;
        Random rand;
        [OneTimeSetUp]
        public void init() {
            rand = new Random();
            serverProxy = new ServerProxy();
        }

        [TestCase]
        public async Task TestGetUnauthorizedSession()
        {
            SessionType session = await serverProxy.GetUnauthorizedSession();
            Assert.AreEqual(30, session.getSessionKey().Length);
        }

        [TestCase]
        public async Task TestLogin()
        {
            string email = rand.Next().ToString();
            string password = rand.Next().ToString();
            string suffix = rand.Next().ToString();

            CreateAccountResultType createAccountResult = await serverProxy.CreateAccount(
            makeRandomCreateAccountData(nameSuffix: suffix, email: email, password: password));
            Assert.AreEqual(true, createAccountResult.IsSuccess());

            LoginResultType loginResultType = await serverProxy.LogIn(new LoginData()
            {
                Email = email, Password = password, SessionKey = "zaaaaaa"
            });
            Assert.AreEqual(true, loginResultType.IsSuccess());
        }
        [TestCase]
        public async Task CreateAccountForm()
        {
            string suffix = rand.Next().ToString();
            string email = rand.Next().ToString();
            string password = rand.Next().ToString();
            CreateAccountResultType createAccountResult = await serverProxy.CreateAccount(
            makeRandomCreateAccountData(nameSuffix: suffix, email: email, password: password));
            Assert.AreEqual(true, createAccountResult.IsSuccess());
        }

        [TestCase]
        public async Task SendTask() {
            string TaskName = rand.Next().ToString();
            string suffix = rand.Next().ToString();
            string email = rand.Next().ToString();
            string password = rand.Next().ToString();
            CreateAccountResultType createAccountResult = await serverProxy.CreateAccount(
            makeRandomCreateAccountData(nameSuffix: suffix, email: email, password: password));
            Assert.AreEqual(true, createAccountResult.IsSuccess());

            SendTaskResultType result = await serverProxy.SendTask(
                makeRandomTask(TaskName, createAccountResult.SessionKey));
            Assert.AreEqual(true, result.IsSuccess());
        }


        [TestCase]
        public async Task GetAllTasks()
        {
            string suffix = rand.Next().ToString();
            string email = rand.Next().ToString();
            string password = rand.Next().ToString();
            CreateAccountResultType createAccountResult = await serverProxy.CreateAccount(
            makeRandomCreateAccountData(nameSuffix: suffix, email: email, password: password));
            Assert.AreEqual(true, createAccountResult.IsSuccess());

            string TaskName = rand.Next().ToString();
            SendTaskResultType result = await serverProxy.SendTask(
                makeRandomTask(TaskName, createAccountResult.SessionKey));
            Assert.AreEqual(true, result.IsSuccess());

            
            RetrieveTaskResultType retrieveTaskResultType = 
                await serverProxy.GetAllTasks(createAccountResult.SessionKey);
            Assert.AreEqual(true, retrieveTaskResultType.IsSuccess());
            Assert.AreEqual(1, retrieveTaskResultType.RetrieveTaskListResult.Length);
        }

        private CreateAccountData makeRandomCreateAccountData(string nameSuffix, string email, string password) {
            return new CreateAccountData()
            {
                FirstName = "TestFirstName" + nameSuffix,
                MiddleName = "TestMiddleName" + nameSuffix,
                LastName = "TestLastName" + nameSuffix,
                Email = email,
                Password = password,
                SessionKey = "zaaaaaa"
            };
        }
        private TaskData makeRandomTask(string taskName, string sessionKey){
            return new TaskData()
            {
                TaskName = rand.Next().ToString(),
                TimeSpent = (float)2.5,
                TaskDateTime = DateTime.UtcNow,
                SessionKey = sessionKey,
            };
        }
    }
}
