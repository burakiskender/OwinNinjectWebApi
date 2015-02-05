using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

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

        //todo Some resources to check still:
        //https://bertt.wordpress.com/2014/01/15/unit-test-web-api-methods-with-in-memory-httpserver/
        //http://blogs.msdn.com/b/kiranchalla/archive/2012/05/06/in-memory-client-amp-host-and-integration-testing-of-your-web-api-service.aspx
        //http://www.strathweb.com/2012/06/asp-net-web-api-integration-testing-with-in-memory-hosting/
        //https://pfelix.wordpress.com/2012/03/05/asp-net-web-api-in-memory-hosting/

        //Startup - Global configuration
        //http://blog.kloud.com.au/2014/10/26/asp-net-web-api-integration-testing-with-one-line-of-code/

        //Overall idea:
        //http://chimera.labs.oreilly.com/books/1234000001708/ch17.html#_the_role_of_unit_testing_in_test_driven_development

        [Test]
        public void Values_With20Repetitions_ReturnBigString()
        {
            var response = _server.HttpClient.GetAsync("/api/values/20").Result;
            var result = response.Content.ReadAsAsync<string>().Result;

            Assert.AreEqual("AAAAAAAAAAAAAAAAAAAA", result);
        }

        [Test]
        public async void Basic_Api_Using_Owin_InMemory()
        {
            //http://blogs.msdn.com/b/webdev/archive/2013/11/26/unit-testing-owin-applications-using-testserver.aspx

            using (var server = TestServer.Create(app =>
            {
                //app.UseErrorPage(); // See Microsoft.Owin.Diagnostics
                //app.UseWelcomePage("/Welcome"); // See Microsoft.Owin.Diagnostics
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Hello world using OWIN TestServer");
                });
            }))
            {
                HttpResponseMessage response = await server.CreateRequest("/").AddHeader("header1", "headervalue1").GetAsync();

                //Execute necessary tests
                Assert.AreEqual("Hello world using OWIN TestServer", await response.Content.ReadAsStringAsync());
            }
        }

        [Test]
        public void Test()
        {
            //arrange
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { routetemplate = "Version", id = RouteParameter.Optional });

            var server = new HttpServer(config);
            var client = new HttpClient(server);

            // act
            var response = client.GetAsync("http://server/api/values/20").Result;

            //assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            var answer = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(answer.Contains("AA"));
        }

        [Test]
        public void WebApiGetAllTest()
        {
            var response = _server.HttpClient.GetAsync("/api/test").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<string>>().Result.ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.First());
            Assert.AreEqual("value2", result.Last());
        }
    }
}
