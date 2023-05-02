using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using StudentInformationSystem.Controllers;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Services.Interfaces;
using StudentInformationSystem.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace StudentInformationSystem.Tests.Tests
{
    public class AdminControllerTest
    {
        [Fact]
        public async void DeleteUserAsync_WhenAppropriateUserIdIsGiven_DeletesUserSuccesfully()
        {
            // Arrange
            var adminServiceStub = new Mock<IAdminService>();
            adminServiceStub.Setup(repo => repo.DeleteUserAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            AdminController adminController = new(adminServiceStub.Object);
            //Act
            var result = await adminController.DeleteUserAsync(Guid.NewGuid());
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void DeleteUserAsync_WhenInvalidUserIdIsGiven_ReturnsBadRequest()
        {
            // Arrange
            var adminServiceStub = new Mock<IAdminService>();
            adminServiceStub.Setup(repo => repo.DeleteUserAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseDto(false, "This User doesn't have Admin rights"));
            AdminController adminController = new(adminServiceStub.Object);
            //Act
            var result = await adminController.DeleteUserAsync(Guid.NewGuid());

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);              
        }
    }
}
