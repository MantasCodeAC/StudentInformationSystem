using StudentInformationSystem.Repository.Model.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonAsync(Guid personId);
        Task<Person> GetPersonAsync(int PersonalCode);
        Task<Residence> GetResidenceAsync(Guid personId);
        Task<ProfilePicture> GetProfilePictureByPersonAsync(Guid personId);
        Task CreatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
        Task UpdatePersonAsync();
        Task<Person> GetPersonByUserAsync(Guid userId);
    }
}
