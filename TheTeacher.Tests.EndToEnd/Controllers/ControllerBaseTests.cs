using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using TheTeacher.Api;

namespace TheTeacher.Tests.EndToEnd.Controllers
{
    public abstract class ControllerBaseTests
    {
        protected TestServer Server;
        protected HttpClient Client;
        
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Server = new TestServer( new WebHostBuilder()
                        .UseStartup<Startup>()
                        .UseConfiguration(configuration));
            
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}