using Moq;
using NUnit.Framework;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Tests.Services
{
    [TestFixture]
    public class AvailableTimePeriodServiceTests
    {
        private Mock<ITeacherRepository> teacherRepositoryMock;
        private AvailableTimePeriodService availableTimePeriodService;

        [SetUp]
        public void SetUp()
        {
            teacherRepositoryMock = new Mock<ITeacherRepository>();
            availableTimePeriodService = new AvailableTimePeriodService(teacherRepositoryMock.Object);
        } 

        
    }
}