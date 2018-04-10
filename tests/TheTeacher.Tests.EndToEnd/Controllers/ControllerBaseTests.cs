using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using TheTeacher.Api;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Tests.EndToEnd.Controllers
{
    [TestFixture]
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

        protected async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        protected async Task<string> GetTokenAsync(string email, string password)
        {
            var command = new LoginUser
            {
                Email = email,
                Password = password
            };

            var payload = GetPayload(command);
            var response = await Client.PostAsync("/login", payload);
            var responseString = await response.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<JwtDto>(responseString);

            return jwt.Token;  
        }        
        
        protected RequestBuilder CreateRequest(string path, StringContent payload, IDictionary<string,string> headers)
        {
            var request = Server.CreateRequest(path);
            foreach(var pair in headers)
            {
                request.AddHeader(pair.Key,pair.Value);
            }
            request.And(x => x.Content = payload);
            return request;
        }

        // Returns an anonymous type with exception code and message
        protected async Task<(string errorCode, string errorMessage)> GetExceptionCodeAndMessageAsync(HttpResponseMessage response) 
        {

            var definition = new { code = "", message = "" };
            var responseString = await response.Content.ReadAsStringAsync();
            var exceptionMessage = JsonConvert.DeserializeAnonymousType(responseString, definition);
            return (exceptionMessage.code, exceptionMessage.message);
        }
    }
}