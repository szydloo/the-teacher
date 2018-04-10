// using System;
// using System.Threading.Tasks;
// using AutoMapper;
// using Moq;
// using NUnit.Framework;
// using TheTeacher.Core.Domain;
// using TheTeacher.Infrastructure.Repositories;
// using TheTeacher.Infrastructure.Services;

// namespace TheTeacher.Tests.Services
// {
//     [TestFixture]
//     public class AvailableTimePeriodServiceTests
//     {
//         private Mock<ITeacherRepository> teacherRepositoryMock;
//         private AvailableTimePeriodService availableTimePeriodService;
//         private Mock<IMapper> mapperMock;
//         private Mock<Teacher> teacherMock;

//         [SetUp]
//         public void SetUp()
//         {
//             teacherRepositoryMock = new Mock<ITeacherRepository>();
//             mapperMock = new Mock<IMapper>();
//             availableTimePeriodService = new AvailableTimePeriodService(teacherRepositoryMock.Object, mapperMock.Object);
//             teacherMock = new Mock<Teacher>();
            
//         } 

//         [Test]
//         public async Task adding_time_period_should()
//         {
//             // Arrange
//             var id = Guid.NewGuid();
            
//             var start = new DateTime(2014,12,12,12,12,00);
//             var end = new DateTime(2014,12,12,13,12,00);
//             var tcs = new TaskCompletionSource<Teacher>();
//             tcs.SetResult(new Teacher(new User(id,"email@email.com","password","salt","username","user"),new Address("testStreet","testCity","testZipcode","testCountry"),"fullname"));

//             teacherRepositoryMock.Setup(x => x.GetAsync(id)).Returns(tcs.Task);

//             // Act
//             await availableTimePeriodService.AddTimePeriodAsync(teacherMock.Object.UserID,start,end);

//             // Assert
//             teacherMock.Verify(x => x.AddAvailableTimePeriod(start,
//                 end),Times.Once());
            
//         }
        
//     }
// }