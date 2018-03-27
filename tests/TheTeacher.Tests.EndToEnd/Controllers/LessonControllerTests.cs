using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TheTeacher.Infrastructure.Commands.LessonCom;
using TheTeacher.Infrastructure.Exceptions;

namespace TheTeacher.Tests.EndToEnd.Controllers
{
    [TestFixture]
    public class LessonControllerTests : ControllerBaseTests
    {
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
            exceptionMessage.errorCode.ShouldBeEquivalentTo(ServiceErrorCodes.TeacherNotFound);
        }

        [Test]
        public async Task deleting_lesson_should_return_no_content()
        {
            var email = "test2@email.com";
            var password = "secret2";
            var user = await GetUserAsync(email);
            var token = await GetTokenAsync(email, password);

            var command = new DeleteLesson
            {
                Name = "Biology",
            };

            var payload = GetPayload(command);

            var request = CreateRequest($"http://localhost:5000/lessons/{command.Name}", payload,
                new Dictionary<string,string>
                {
                    { "Authorization", $"Bearer {token}" },
                    { "Content-type", $"application/json" }
                }
            );

            var response = await request.SendAsync("DELETE");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }
    }
}