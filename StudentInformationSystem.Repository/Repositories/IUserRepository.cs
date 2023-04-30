using StudentInformationSystem.Repository.Model.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username);
        Task<User> GetUserAsync(Guid userId);
        Task SaveUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
