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
    public class UserSeedDataFixture : IDisposable
    {
        public StudentInformationSystemDbContext dbContext { get; private set; }
        public UserSeedDataFixture()
        {
            var options = new DbContextOptionsBuilder<StudentInformationSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "StudentInformationSystem")
                .Options;
            dbContext = new StudentInformationSystemDbContext(options);
            dbContext.Users.Add(new User
            {
                Id = Guid.Parse("2C3972FB-E28E-4CA8-A249-4530307951C7"),
                PasswordHash = Encoding.ASCII.GetBytes("0x8345DB3"),
                PasswordSalt = Encoding.ASCII.GetBytes("0xB6D3708"),
                Username = "Jonas",
                role = new User.Role(),
            });
            dbContext.Users.Add(new User
            {
                Id = Guid.Parse("52CC557A-4B74-4380-A8CD-BD4600E0DE3B"),
                PasswordHash = Encoding.ASCII.GetBytes("0x8345DB31"),
                PasswordSalt = Encoding.ASCII.GetBytes("0xB6D37081"),
                Username = "Jurgis",
                role = new User.Role(),
            });
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
