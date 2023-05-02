using Microsoft.Extensions.Configuration;
using Moq;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Services;
using StudentInformationSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Tests.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public async void SignupAsync_WithNewUser_SignsUpUser()
        {
            // Arrange
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<String>()));
            var configurationStub = new Mock<IConfiguration>();
            UserService userService = new (repositoryStub.Object, configurationStub.Object);
            //Act
            var result = await userService.SignupAsync("", "");
            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async void SignupAsync_WithExistingUser_ReturnsFalse()
        {
            // Arrange
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<String>()))
                .ReturnsAsync(CreateRandomUser());               
            var configurationStub = new Mock<IConfiguration>();
            UserService userService = new(repositoryStub.Object, configurationStub.Object);
            //Act
            var result = await userService.SignupAsync("", "");
            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async void LoginAsync_WithNotExistingUser_LoginsUser()
        {
            // Arrange
            var repositoryStub = new Mock<IUserRepository>();
            repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<String>()));
            var configurationStub = new Mock<IConfiguration>();
            UserService userService = new(repositoryStub.Object, configurationStub.Object);
            //Act
            var result = await userService.LoginAsync("", "");
            // Assert
            Assert.False(result.IsSuccess);
        }

        private User CreateRandomUser()
        {
            return new User()
            {
                Id = Guid.Parse("2C3972FB-E28E-4CA8-A249-4530307951C7"),
                PasswordHash = Encoding.ASCII.GetBytes("0x8345DB"),
                PasswordSalt = Encoding.ASCII.GetBytes("0xB6D3708"),
                Username = "Jonas",
                role = new User.Role()
            };
        }
    }
}
