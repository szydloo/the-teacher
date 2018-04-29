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
    public class SubjectsControllerTests : ControllerBaseTests
    {
  
        [Test]
        public async Task getting_subjects_should_return_subjects()
        {
            var response = await Client.GetAsync($"/subjects/");

            response.Should().Be(true);
        }
    }
}