using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Data
{
    public class StudentInformationSystemDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public StudentInformationSystemDbContext(DbContextOptions<StudentInformationSystemDbContext> options) : base(options)
        {

        }
    }
}
