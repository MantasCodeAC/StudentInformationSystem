using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentInformationSystem.Controllers;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Tests.Tests
{
    public class UserControllerTest
    {
        [Fact]
        public async void SignupAsync_WhenAppropriateUserInfoIsGiven_SingsUpUserSuccesfully()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(repo => repo.SignupAsync(It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            UserController userController = new(userServiceStub.Object);
            //Act
            var result = await userController.SignupAsync(new UserDto() { Username = "", Password = "" });
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void SignupAsync_WhenInvalidUserInfoIsGiven_ReturnsBadRequest()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(repo => repo.SignupAsync(It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(false, "User already exists"));
            UserController userController = new(userServiceStub.Object);
            //Act
            var result = await userController.SignupAsync(new UserDto(){Username = "", Password = ""});
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void LoginAsync_WhenAppropriateUserInfoIsGiven_ReturnsOkObjectResult()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(repo => repo.LoginAsync(It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            UserController userController = new(userServiceStub.Object);
            //Act
            var result = await userController.LoginAsync(new UserDto() { Username = "", Password = "" });
            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
        [Fact]
        public async void LoginAsync_WhenInvalidUserInfoIsGiven_ReturnsBadRequest()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(repo => repo.LoginAsync(It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(false, "Invalid User"));
            UserController userController = new(userServiceStub.Object);
            //Act
            var result = await userController.LoginAsync(new UserDto() { Username = "", Password = "" });
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }
    }
}
