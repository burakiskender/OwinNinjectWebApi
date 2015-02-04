using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace OwinNinjectProject.Integration.Tests
{
    [TestFixture]
    public class ValuesControllerIntegrationTests
    {
        private TestServer _server;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [Test]
        public void Values_With20Repetitions_ReturnBigString()
        {
            var response = _server.HttpClient.GetAsync("/api/values/20").Result;
            var result = response.Content.ReadAsAsync<string>().Result;

            Assert.AreEqual("AAAAAAAAAAAAAAAAAAAA", result);
        }

        //[Test]
        //public void WebApiGetAllTest()
        //{
        //    var response = _server.HttpClient.GetAsync("/api/test").Result;
        //    var result = response.Content.ReadAsAsync<IEnumerable<string>>().Result;

        //    Assert.AreEqual(2, result.Count());
        //    Assert.AreEqual("hello", result.First());
        //    Assert.AreEqual("world", result.Last());
        //}
    }
}
