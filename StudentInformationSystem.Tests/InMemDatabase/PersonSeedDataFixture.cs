using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Repository.Data;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Tests.InMemDatabase
{
    public class PersonSeedDataFixture:IDisposable
    {
        public StudentInformationSystemDbContext dbContext { get; private set; }
        public PersonSeedDataFixture()
        {
            var options = new DbContextOptionsBuilder<StudentInformationSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "StudentInformationSystem")
                .Options;
            dbContext = new StudentInformationSystemDbContext(options);
            dbContext.Users.Add(new User
            {
                Id = Guid.Parse("4093AED6-FEF5-47BB-9780-DBE7D53CAB90"),
                PasswordHash = Encoding.ASCII.GetBytes("0x8345DB311"),
                PasswordSalt = Encoding.ASCII.GetBytes("0xB6D370811"),
                Username = "Antanas",
                role = new User.Role(),
            });
            dbContext.Persons.Add(new Person
            {
                PersonID = Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA15"),
                FirstName = "Tomas",
                LastName = "Tomauskas",
                PersonalCode = 123654789,
                PhoneNumber = "+370654879321",
                Email = "jj@gmail.com",
                UserId = Guid.Parse("4093AED6-FEF5-47BB-9780-DBE7D53CAB90")
            });
            dbContext.Persons.Add(new Person
            {
                PersonID = Guid.Parse("3962D0CA-A554-4FBA-9960-6732CA8459E6"),
                FirstName = "Lukas",
                LastName = "Lukauskas",
                PersonalCode = 1236547898,
                PhoneNumber = "+370654879321",
                Email = "jj@gmail.com",
                UserId = Guid.Parse("54E75ACF-9562-4054-9AD6-DD35119DFEEA")
            });
            dbContext.Residences.Add(new Residence
            {
                Id = Guid.Parse("5949DBD9-255B-4ABE-8810-0425A4C10A62"),
                City = "Kaunas",
                Street = "Savanoriu g.",
                HouseNumber = 100,
                ApartmentNumber = 1,
                PersonID = Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA15")
            });
            dbContext.ProfilePictures.Add(new ProfilePicture
            {
                Id = Guid.Parse("23F795DF-458D-4FC9-A22A-D30FEDFE8141"),
                Name = "TestPicture.jpg",
                Data = Encoding.ASCII.GetBytes("0x8345DB3"),
                PersonID = Guid.Parse("ED8F6953-5915-4D57-B261-56BAAB6FAA15")
            });
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
