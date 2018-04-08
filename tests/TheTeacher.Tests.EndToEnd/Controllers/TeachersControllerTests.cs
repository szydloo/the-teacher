using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using TheTeacher.Infrastructure.Commands.LessonCom;
using TheTeacher.Infrastructure.Commands.Teacher;
using TheTeacher.Infrastructure.Commands.User;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Exceptions;

namespace TheTeacher.Tests.EndToEnd.Controllers
{
    [TestFixture]
    public class TeachersControllerTests : ControllerBaseTests
    {

        [Test]
        public async Task getting_teacher_with_invalid_email_should_not_found()
        {
            string email = "test14@email.com";
            var user = await GetUserAsync(email);

            var response = await Client.GetAsync($"/teachers/{user.Id}");
            var responseMessage = await GetExceptionCodeAndMessageAsync(response);

            responseMessage.errorCode.ShouldBeEquivalentTo(ServiceErrorCodes.TeacherNotFound); 
            
        }

        [Test]
        public async Task creating_teacher_given_valid_credentials_should_return_no_content()
        {
            string email = "test15@email.com";
            string password = "secret15";
            var user = await GetUserAsync(email);
            var token = await GetTokenAsync(email, password);
            var command = new CreateTeacher
            {
                Street = "testStreet",
                City = "testCity",
                Zipcode = "00-000",
                Country = "testCountry",
                Fullname = "Test Test"
            };
            var payload = GetPayload(command);

            var request = CreateRequest($"http://localhost:5000/teachers", payload,
                new Dictionary<string,string>
            {
                {"Authorization", $"Bearer {token}"},
                {"Content-type", "application/json"}
            });

            var response = await request.PostAsync();
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ShouldBeEquivalentTo($"/teachers/{user.Id}");
        }

        [Test]
        public async Task getting_teacher_with_valid_email_should_return_no_content()
        {
            string email = "test10@email.com";
            var user = await GetUserAsync(email);

            var response = await Client.GetAsync($"/teachers/{user.Id}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

 
    }
}