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

        public void CreatePersonAsync(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void DeletePersonAsync(Person person)
        {
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }

        public ProfilePicture GetProfilePictureByPersonAsync(Guid personId)
        {
            return _context.ProfilePictures.SingleOrDefault(x => x.PersonID==personId);
        }

        public Person GetPersonAsync(Guid personId)
        {
            return _context.Persons.SingleOrDefault(x => x.PersonID == personId);
        }

        public Person GetPersonAsync(int PersonalCode)
        {
            return _context.Persons.SingleOrDefault(x => x.PersonalCode == PersonalCode);
        }
        public Person GetPersonByUserAsync(Guid userId)
        {
            return _context.Persons.SingleOrDefault(x => x.UserId == userId);
        }

        public Residence GetResidenceAsync(Guid personId)
        {
            return _context.Residences.SingleOrDefault(x => x.PersonID == personId);
        }

        public void UpdatePersonAsync()
        {
            _context.SaveChanges();
        }
    }
}
