using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using StudentInformationSystem.Repository.Data;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Tests.InMemDatabase;
using System.Text;

namespace StudentInformationSystem.Tests.Tests
{
    public class UserRepositoryTests : IClassFixture<UserSeedDataFixture>
    {
        private readonly UserSeedDataFixture fixture;
        public UserRepositoryTests(UserSeedDataFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public async Task GetUserAsync_WithUnexistingId_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            UserRepository userRepository = new(context);

            //Act
            var result = await userRepository.GetUserAsync(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserAsync_WithExistingId_ReturnsUser()
        {
            //Arrange
            var context = fixture.dbContext;
            UserRepository userRepository = new(context);

            //Act
            var result = await userRepository.GetUserAsync(Guid.Parse("2C3972FB-E28E-4CA8-A249-4530307951C7"));

            //Assert
            Assert.Equal("Jonas", result.Username);

        }

        [Fact]
        public async Task GetUserAsync_WithUnexistingUsername_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            UserRepository userRepository = new(context);

            //Act
            var result = await userRepository.GetUserAsync("");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserAsync_WithExistingUsername_ReturnsUser()
        {
            //Arrange
            var context = fixture.dbContext;
            UserRepository userRepository = new(context);

            //Act
            var result = await userRepository.GetUserAsync("Jonas");

            //Assert
            Assert.Equal("Jonas", result.Username);
        }

        [Fact]
        public async Task SaveUserAsync_WithNewUser_SavesNewUserToDatabase()
        {
            //Arrange
            var context = fixture.dbContext;
            UserRepository userRepository = new(context);

            //Act           
            User user = new User()
            {
                Id = Guid.Parse("54E75ACF-9562-4054-9AD6-DD35119DFEEA"),
                PasswordHash = Encoding.ASCII.GetBytes("0x8345DB34"),
                PasswordSalt = Encoding.ASCII.GetBytes("0xB6D37085"),
                Username = "Petras",
                role = new User.Role()
            };
            await userRepository.SaveUserAsync(user);

            //Assert
            Assert.Equal("Petras", (await userRepository.GetUserAsync("Petras")).Username);
        }

        [Fact]
        public async Task DeleteUserAsync_WithExistingUser_DeletesUserFromDatabase()
        {
            //Arrange
            var context = fixture.dbContext;
            UserRepository userRepository = new(context);

            //Act           
            var userToDelete = await userRepository.GetUserAsync(Guid.Parse("52CC557A-4B74-4380-A8CD-BD4600E0DE3B"));
            await userRepository.DeleteUserAsync(userToDelete);
            //Assert
            Assert.Null(await userRepository.GetUserAsync(Guid.Parse("52CC557A-4B74-4380-A8CD-BD4600E0DE3B")));
        }
    }
}