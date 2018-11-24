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
            DateTime now = DateTime.UtcNow;



            CreateAccountResultType createAccountResult = await serverProxy.CreateAccount(
            makeRandomCreateAccountData(nameSuffix: suffix, email: email, password: password));
            Assert.AreEqual(true, createAccountResult.IsSuccess());

            string TaskName = rand.Next().ToString();
            TaskData task = makeRandomTask(TaskName, createAccountResult.SessionKey);
            task.TaskDateTime = now;
            
            SendTaskResultType result = await serverProxy.SendTask(
                makeRandomTask(TaskName, createAccountResult.SessionKey));
            Assert.AreEqual(true, result.IsSuccess());


            RetrieveTaskResultType retrieveTaskResultType =
                await serverProxy.GetAllTasks(new RetrieveTaskListData()
                {
                    SessionKey = createAccountResult.SessionKey
                });
            Assert.AreEqual(true, retrieveTaskResultType.IsSuccess());
            Assert.AreEqual(1, retrieveTaskResultType.TaskList.Length);
            Assert.AreEqual(TaskName, retrieveTaskResultType.TaskList[0].TaskName);
            Assert.AreEqual(now.Year, retrieveTaskResultType.TaskList[0].getDateTime().Year);
            Assert.AreEqual(now.Month, retrieveTaskResultType.TaskList[0].getDateTime().Month);
            Assert.AreEqual(now.Day, retrieveTaskResultType.TaskList[0].getDateTime().Day);
            Assert.AreEqual(now.Hour, retrieveTaskResultType.TaskList[0].getDateTime().Hour);
            Assert.AreEqual(now.Minute, retrieveTaskResultType.TaskList[0].getDateTime().Minute);
            //Assert.AreEqual(now.Second, retrieveTaskResultType.TaskList[0].getDateTime().Second);
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
                TaskName = taskName,
                TimeSpent = (float)2.5,
                TaskDateTime = DateTime.UtcNow,
                SessionKey = sessionKey,
            };
        }
    }
}
