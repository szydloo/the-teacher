using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : ControllerBaseTests
    {
        [Test]
        public async Task given_valid_email_user_should_exist()
        {
            var email = "test1@email.com";
            var response = await Client.GetAsync($"users/{email}");

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Test]
        public async Task registering_user_with_valid_data_should_be_ok()
        {

            var command = new CreateUser
            {
                Email = "test@email.com",
                Password = "secretto",
                Username = "usernemmo",
                Fullname = "Markitto Robertto",
                Role = "user"
            };
            
            var payload = GetPayload(command);
            var response = await Client.PostAsync($"users",payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().ShouldBeEquivalentTo($"/users/{command.Email}");

            var user = await GetUserAsync(command.Email);
            user.Fullname.ShouldBeEquivalentTo(command.Fullname);
        }

        [Test]
        public void registering_user_with_email_without_at_should_throw_exception()
        {
            var command = new CreateUser
            {
                Email = "testinvalid.com",
                Password = "secret",
                Username = "usernemmo",
                Fullname = "Markitto Robertto",
                Role = "user"
            };

            var payload = GetPayload(command);

            // Fluent assertions exceptions
            Func<Task> act = Client.Awaiting( async x => await x.PostAsync("users", payload));
            act.ShouldThrow<Exception>().And
                .Message.Contains("Invalid email format");
        }

        [Test]
        public void registering_user_with_already_existing_email_should_throw_exception()
        {
            var command = new CreateUser
            {
                Email = "test1@email.com",
                Username = "username",
                Password = "secret",
                Fullname = "Full Name",
                Role = "user"
            };
            
            var payload = GetPayload(command);
            Func<Task> act = Client.Awaiting( async x => await x.PostAsync("users", payload));
            act.ShouldThrow<Exception>().And
                .Message.Contains($"User with this email: {command.Email}");
        }

        [Test]
        public async Task getting_user_that_does_not_exist_should_return_not_found()
        {
            var email = "emailRandom@gmail.com";
            var response = await Client.GetAsync($"users/{email}");

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task changing_existing_user_password_without_token_should_return_unauthorized()
        {
            var user = await GetUserAsync("test1@email.com");
            var newPassword = "secret420";
            var command = new ChangeUserPassword
            {
                UserId = user.UserId,
                CurrentPassword = user.Password,
                NewPassword = newPassword
            };
            var payload = GetPayload(command);
            var response = await Client.PutAsync($"users/password", payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Unauthorized);
        }

        // [Test]
        // public async Task changing_existing_user_password_with_token_should_return_nocontent()
        // {
        //     var email = "test1@email.com";
        //     var password = "secret1";
        //     var user = await GetUserAsync(email);
        //     var token = await GetTokenAsync(email, password);

        //     var newPassword = "secret420";

        //     var command = new ChangeUserPassword
        //     {
        //         UserId = user.UserId,
        //         CurrentPassword = user.Password,
        //         NewPassword = newPassword
        //     };
        //     var payload = GetPayload(command);
        
        //     var request = new HttpRequestMessage()
        //     var response = await Client;

        //     response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        // }

        [Test]
        public async Task deleteing_user_without_token_should_return_unauthorized()
        {
            var response = await Client.DeleteAsync("users/me");

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Unauthorized);
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            var command = new LoginUser
            {
                Email = email,
                Password = password
            };

            var logPayload = GetPayload(command);
            var response = await Client.PostAsync("/login", logPayload);
            var contentMessage = await response.Content.ReadAsStringAsync();
            var splitedMessage = contentMessage.Split('"'); 
            return splitedMessage[3];         
            
        }
        public async Task<UserDTO> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDTO>(responseString);
        }
    }
}