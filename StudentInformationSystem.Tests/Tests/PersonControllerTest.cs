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
    public class PersonControllerTest
    {
        [Fact]
        public async void CreatePersonsync_WhenAppropriatePersonInfoIsGiven_CreatesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.AddPersonAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<String>(),
                It.IsAny<String>(), It.IsAny<ImageUploadRequest>(), It.IsAny<ResidenceDto>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.CreatePersonAsync(new PersonDto());
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void CreatePersonsync_WhenInvalidPersonInfoIsGiven_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.AddPersonAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Int32>(), It.IsAny<String>(),
                It.IsAny<String>(), It.IsAny<ImageUploadRequest>(), It.IsAny<ResidenceDto>()))
                .ReturnsAsync(new ResponseDto(false, "Person already exists in a database"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.CreatePersonAsync(new PersonDto());
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void GetPersonInfoAsync_WhenAppropriatePersonIDIsGiven_ReturnsPersonInfoSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(x => x.GetPersonInfoAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseDto(true, "PersonInfo returned successfully"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.GetPersonInfoAsync(Guid.NewGuid());
            // Assert
            Assert.Equal("PersonInfo returned successfully", result.Value);
        }
        [Fact]
        public async void GetPersonInfoAsync_WhenInvalidPersonIDIsGiven_ReturnsBadRequestResult()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(x => x.GetPersonInfoAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseDto(false, "Person couldn't be found by this ID"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.GetPersonInfoAsync(Guid.NewGuid());
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyNameAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonNameAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyNameAsync("");
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyNameAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonNameAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyNameAsync("");
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyLastNameAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonLastNameAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyLastNameAsync("");
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyLastNameAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonLastNameAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyLastNameAsync("");
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyPersonalCodeAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonalCodeAsync(It.IsAny<Int32>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPersonalCodeAsync(1);
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyPersonalCodeAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonalCodeAsync(It.IsAny<Int32>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPersonalCodeAsync(1);
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyEmailAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonEmailAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyEmailAsync("");
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyEmailAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonEmailAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyEmailAsync("");
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyPhoneAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonPhoneAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPhoneAsync("");
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyPhoneAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonPhoneAsync(It.IsAny<String>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPhoneAsync("");
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyPersonResidenceAddressAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonResidenceAsync(It.IsAny<ResidenceDto>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPersonResidenceAddressAsync(new ResidenceDto());
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyPersonResidenceAddressAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdatePersonResidenceAsync(It.IsAny<ResidenceDto>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPersonResidenceAddressAsync(new ResidenceDto());
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void ModifyPersonProfilePictureAsync_WhenPersonNotExist_ReturnsBadRequest()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdateProfilePictureAsync(It.IsAny<ImageUploadRequest>()))
                .ReturnsAsync(new ResponseDto(true, "Success"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPersonProfilePictureAsync(new ImageUploadRequest());
            // Assert
            Assert.True(result.Value.IsSuccess);
        }
        [Fact]
        public async void ModifyPersonProfilePictureAsync_WhenAppropriatePersonInfoIsGiven_ModifiesPersonSuccesfully()
        {
            // Arrange
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(repo => repo.UpdateProfilePictureAsync(It.IsAny<ImageUploadRequest>()))
                .ReturnsAsync(new ResponseDto(false, "This User's Personal information is empty"));
            PersonController personController = new(personServiceStub.Object);
            //Act
            var result = await personController.ModifyPersonProfilePictureAsync(new ImageUploadRequest());
            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
    }
}
