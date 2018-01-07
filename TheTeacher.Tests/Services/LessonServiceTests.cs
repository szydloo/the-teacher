using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Tests.Services
{
    public class LessonServiceTests
    {
        Mock<ITeacherRepository> teacherRepositoryMock;
        Mock<IMapper> mapperMock;
        Mock<ISubjectProvider> subjectProviderMock;
        ILessonService lessonService;
        
        [SetUp]
        public void Setup()
        {
            teacherRepositoryMock = new Mock<ITeacherRepository>();
            mapperMock = new Mock<IMapper>();
            subjectProviderMock = new Mock<ISubjectProvider>();
            lessonService = new LessonService(mapperMock.Object, teacherRepositoryMock.Object, subjectProviderMock.Object);
        }

        [Test]
        public async Task adding_lesson_should_invoke_get_lesson_from_sp_once()
        {
            // Arrange
            var id = Guid.NewGuid();
            var tcs = new TaskCompletionSource<Teacher>();
            tcs.SetResult(new Teacher(new User(id,"email@email.com","test","test","test","user"),"testtest","TestTest"));
            teacherRepositoryMock.Setup( x => x.GetAsync(It.IsAny<Guid>())).Returns(tcs.Task);

            var tcs2 = new TaskCompletionSource<SubjectDTO>();
            tcs2.SetResult(new SubjectDTO
            {
                Name = "Biology",
                Category = "Science"
            });

            subjectProviderMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(tcs2.Task);

            // Act
            await lessonService.AddAsync(id, "Biology", "Science", "Elementary", 10M);

            // Assert
            subjectProviderMock.Verify(x => x.GetAsync("Biology", "Science"), Times.Once());
            teacherRepositoryMock.Verify(y => y.GetAsync(It.Is<Guid>(fu => fu.Equals(id))));
        }

        [Test]
        public async Task getting_teachers_with_specific_lesson_should()
        {
            // Arrange
            var id = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var t1 = new Teacher(new User(id,"email@email.com","test","test","test","user"),"testtest","TestTest");
            var t2 = new Teacher(new User(id2,"email2@email.com","test2","test2","test2","user"),"testtest2","TestTest");
            
            var tcs = new TaskCompletionSource<IEnumerable<Teacher>>();
            tcs.SetResult(new HashSet<Teacher>
                {
                    t1, t2
                });
            teacherRepositoryMock.Setup( x => x.GetAllAsync()).Returns(tcs.Task);

            var tcs2 = new TaskCompletionSource<SubjectDTO>();
            tcs2.SetResult(new SubjectDTO
            {
                Name = "Biology",
                Category = "Science"
            });
            
            subjectProviderMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(tcs2.Task);


            // Act
            await lessonService.GetTeachersWithLessonAsync("Biolog");

            // Assert
            teacherRepositoryMock.Verify(y => y.GetAllAsync(), Times.Once());
        }
    }
}