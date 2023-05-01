using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Repository.Data;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly StudentInformationSystemDbContext _context;
        public PersonRepository(StudentInformationSystemDbContext context)
        {
            _context = context;
        }

        public async Task CreatePersonAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<ProfilePicture> GetProfilePictureByPersonAsync(Guid personId)
        {
            return await _context.ProfilePictures.SingleOrDefaultAsync(x => x.PersonID==personId);
        }

        public async Task<Person> GetPersonAsync(Guid personId)
        {
            return await _context.Persons.SingleOrDefaultAsync(x => x.PersonID == personId);
        }

        public async Task<Person> GetPersonAsync(int PersonalCode)
        {
            return await _context.Persons.SingleOrDefaultAsync(x => x.PersonalCode == PersonalCode);
        }
        public async Task<Person> GetPersonByUserAsync(Guid userId)
        {
            return await _context.Persons.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Residence> GetResidenceAsync(Guid personId)
        {
            return await _context.Residences.SingleOrDefaultAsync(x => x.PersonID == personId);
        }

        public async Task UpdatePersonAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
