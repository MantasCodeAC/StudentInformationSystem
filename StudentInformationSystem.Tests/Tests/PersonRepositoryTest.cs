using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Tests.InMemDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Tests.Tests
{
    public class PersonRepositoryTest : IClassFixture<PersonSeedDataFixture>
    {
        private readonly PersonSeedDataFixture fixture;
        public PersonRepositoryTest(PersonSeedDataFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public async Task GetProfilePictureByPersonAsync_WithUnexistingId_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetProfilePictureByPersonAsync(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetProfilePictureByPersonAsync_WithExistingId_ReturnsExpectedPicture()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetProfilePictureByPersonAsync(Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA15"));

            //Assert
            Assert.Equal("TestPicture.jpg", result.Name);
        }
        [Fact]
        public async Task GetPersonAsync_WithUnexistingId_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetPersonAsync(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetPersonAsync_WithExistingId_ReturnsExpectedPerson()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetPersonAsync(Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA15"));

            //Assert
            Assert.Equal("Tomas", result.FirstName);
        }
        [Fact]
        public async Task GetPersonAsync_WithUnexistingPersonalCode_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetPersonAsync(1);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetPersonAsync_WithExistingPersonalCode_ReturnsExpectedPerson()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetPersonAsync(123654789);

            //Assert
            Assert.Equal("Tomas", result.FirstName);
        }
        [Fact]
        public async Task GetPersonByUserAsync_WithUnexistingUserId_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetPersonByUserAsync(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetPersonByUserAsync_WithExistingUserId_ReturnsExpectedPerson()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetPersonByUserAsync(Guid.Parse("4093AED6-FEF5-47BB-9780-DBE7D53CAB90"));

            //Assert
            Assert.Equal("Tomas", result.FirstName);
        }
        [Fact]
        public async Task GetResidenceAsync_WithUnexistingId_ReturnsNull()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetResidenceAsync(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetResidenceAsync_WithExistingId_ReturnsExpectedResidence()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var result = await personRepository.GetResidenceAsync(Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA15"));

            //Assert
            Assert.Equal("5949DBD9-255B-4ABE-8810-0425A4C10A62", result.Id.ToString().ToUpper());
        }
        [Fact]
        public async Task CreatePersonAsync_WithNewPerson_SavesPersonToDatabase()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act           
            Person person = new()
            {
                PersonID = Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA18"),
                FirstName = "Jonas",
                LastName = "Jonauskas",
                PersonalCode = 1236547891,
                PhoneNumber = "+370654879321",
                Email = "jj@gmail.com",
                UserId = Guid.Parse("2C3972FB-E28E-4CA8-A249-4530307951C9")
            };
            await personRepository.CreatePersonAsync(person);

            //Assert
            Assert.Equal("Jonas", (await personRepository.GetPersonAsync(1236547891)).FirstName);
        }
        [Fact]
        public async Task DeletePersonAsync_WithExistingPerson_DeletesPersonFromDatabase()
        {
            //Arrange
            var context = fixture.dbContext;
            PersonRepository personRepository = new(context);

            //Act
            var personToDelete = await personRepository.GetPersonAsync(Guid.Parse("3962D0CA-A554-4FBA-9960-6732CA8459E6"));
            await personRepository.DeletePersonAsync(personToDelete);
            //Assert
            Assert.Null(await personRepository.GetPersonAsync(Guid.Parse("3962D0CA-A554-4FBA-9960-6732CA8459E6")));
        }
    }
}
