using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using TheTeacher.Core.Exceptions;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Exceptions;

namespace TheTeacher.Tests.EndToEnd.Controllers
{
    [TestFixture]
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
                Role = "user",
                Fullname = "Markitto Robertto"                
            };
            
            var payload = GetPayload(command);
            var response = await Client.PostAsync($"users",payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().ShouldBeEquivalentTo($"/users/{command.Email}");

            var user = await GetUserAsync(command.Email);
            user.Fullname.ShouldBeEquivalentTo(command.Fullname);
        }

        [Test]
        public async Task registering_user_with_email_without_at_should_contain_proper_code_and_message()
        {
            var command = new CreateUser
            {
                Email = "testinvalid.com",
                Password = "secret",
                Username = "usernemmo",
                Role = "user",
                Fullname = "Markitto Robertto"
            };

            var payload = GetPayload(command);
            
            var response = await Client.PostAsync("users", payload);
            var exceptionMessage = await GetExceptionCodeAndMessageAsync(response);
            
            exceptionMessage.Item1.ShouldBeEquivalentTo(DomainErrorCodes.InvalidEmail);
            exceptionMessage.Item2.ShouldBeEquivalentTo("Invalid email address.");
        
        }

        [Test]
        public async Task registering_user_with_already_existing_email_should_contain_proper_code_and_message()
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
            var response = await Client.PostAsync("users",payload);
            var exceptionMessage = await GetExceptionCodeAndMessageAsync(response);
            exceptionMessage.Item1.ShouldBeEquivalentTo(ServiceErrorCodes.EmailInUse);
            exceptionMessage.Item2.ShouldBeEquivalentTo($"User with this email: '{command.Email}' already exists.");

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
                UserId = user.Id,
                CurrentPassword = user.Password,
                NewPassword = newPassword
            };
            var payload = GetPayload(command);
            var response = await Client.PutAsync($"users/password", payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task changing_existing_user_password_with_token_should_return_nocontent()
        {
            var email = "test1@email.com";
            var password = "secret1";
            var user = await GetUserAsync(email);
            var token = await GetTokenAsync(email, password);

            var newPassword = "secret420";

            var command = new ChangeUserPassword
            {
                UserId = user.Id,
                CurrentPassword = user.Password,
                NewPassword = newPassword
            };
            var payload = GetPayload(command);

            var request = CreateRequest("http://localhost:5000/users/password", payload,
                new Dictionary<string,string>
                {
                    { "Authorization", $"Bearer {token}" },
                    { "Content-type", $"application/json" }
                }
            );
            var response = await request.SendAsync("PUT");

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }


        [Test]
        public async Task loging_with_correct_data_should_return_token()
        {
            var email = "test1@email.com";
            var password = "secret1";

            var command = new LoginUser
            {
                Email = email,
                Password = password
            };

            var payload = GetPayload(command);
            var response = await Client.PostAsync("/login", payload);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.ReadAsStringAsync().Result.Length.Should().BeGreaterOrEqualTo(300); //TODO create proper validity of token pressence
        }

        [Test]
        public async Task deleteing_user_without_token_should_return_unauthorized()
        {
            var response = await Client.DeleteAsync("users/me");

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Unauthorized);
        }

    }
}