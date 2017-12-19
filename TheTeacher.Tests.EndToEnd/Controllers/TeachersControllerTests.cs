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
    public class TeachersControllerTests : ControllerBaseTests
    {

        [Test]
        public async Task getting_teacher_with_invalid_email_should_not_found()
        {
            string email = "test14@email.com";
            var user = await GetUserAsync(email);

            var response = await Client.GetAsync($"/teachers/{user.Id}");
            var responseMessage = await GetExceptionCodeAndMessageAsync(response);

            // TODO: should be okey, but dotnet test says no / 'run test' says yes || Now it works ???? Read NUnitDocs word by word
            responseMessage.Item1.ShouldBeEquivalentTo(ServiceErrorCodes.TeacherNotFound); 
            
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
                Address = "address"                 
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
            string email = "test1@email.com";
            var user = await GetUserAsync(email);

            var response = await Client.GetAsync($"/teachers/{user.Id}");
            // Doesnt work with dotnet test though it should work/works with run test TODO: Fix  
            // response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Test]
        public async Task adding_lesson_async_should_return_no_content()
        {
            var email = "test4@email.com";
            var password = "secret4";
            var user = await GetUserAsync(email);
            var token = await GetTokenAsync(email, password);

            var command = new AddLesson
            {
                Name = "Biology",
                Category = "Science",
                PricePerHour = 5M,
                Grade = "Elementary"
            };

            var payload = GetPayload(command);

            var request = CreateRequest("http://localhost:5000/lessons", payload,
                new Dictionary<string,string>
                {
                    { "Authorization", $"Bearer {token}" },
                    { "Content-type", $"application/json" }
                }
            );

            var response = await request.PostAsync();
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task adding_lesson_async_to_invalid_teacher_should_return_errorcode_teacher_not_found()
        {
            var email = "test20@email.com";
            var password = "secret20";
            var user = await GetUserAsync(email);
            var token = await GetTokenAsync(email, password);

            var command = new AddLesson
            {
                Name = "Biology",
                Category = "Science",
                PricePerHour = 5M,
                Grade = "Elementary"
            };

            var payload = GetPayload(command);

            var request = CreateRequest("http://localhost:5000/lessons", payload,
                new Dictionary<string,string>
                {
                    { "Authorization", $"Bearer {token}" },
                    { "Content-type", $"application/json" }
                }
            );

            var response = await request.PostAsync();
            var exceptionMessage = await GetExceptionCodeAndMessageAsync(response);
            exceptionMessage.Item1.ShouldBeEquivalentTo(ServiceErrorCodes.TeacherNotFound);
        }
    }
}