using AutoMapper;
using Moq;
using NUnit;
using NUnit.Framework;
using TheTeacher.Core.Domain;
using FluentAssertions;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Services;
using System.Collections.Generic;
using TheTeacher.Infrastructure.Dto;
using TheTeacher.Infrastructure.Settings;
using System;

namespace TheTeacher.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        Mock<IMapper> mapperMock;
        Mock<IUserRepository> userRepositoryMock;
        IEncrypter encrypter;
        IUserService userService;

        [SetUp]
        public void Setup()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper>();
            encrypter = new Encrypter();

            userService = new UserService(mapperMock.Object, encrypter, userRepositoryMock.Object);
        }

        [Test]
        public async Task registering_user_should_invoke_add_async_once()
        {
            await userService.RegisterAsync(Guid.NewGuid(), "email123@gmail.com", "secret", "username1", "user");
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Exactly(1));
        }

        [Test]
        public async Task get_async_when_user_does_not_exist_should_return_null()
        {
            var user = await userService.GetAsync("user@email.com");
            user.Should().BeNull();
        }

        [Test]
        public async Task browse_async_should_be_invoked_once()
        {
            var users = await userService.BrowseAsync();
            userRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            
        }

        [Test]
        public async Task browse_async_should_return_ieunmerable()
        {
            var users = await userService.BrowseAsync();
            users.Should().AllBeAssignableTo<IEnumerable<UserDto>>();
            
        }
    }
}