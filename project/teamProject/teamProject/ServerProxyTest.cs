using System;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace teamProject
{
    class ServerProxyTest
    {
        const string serverIP = "127.0.0.1";
        const string serverPort = "8000";
        Process server;
        ServerProxy serverProxy;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Console.WriteLine("Server binary path: " + Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
            "server.exe"));

            server = Process.Start(
            Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
            "server.exe"));

            serverProxy = new ServerProxy();
            Console.WriteLine("server PID : " + server.Id);
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
            SessionType session = await serverProxy.GetUnauthorizedSession();
            Assert.AreEqual(30, session.getSessionKey().Length);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            server.Kill();
        }
    }
}
