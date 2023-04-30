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
    public class UserRepository: IUserRepository
    {
        private readonly StudentInformationSystemDbContext _context;

        public UserRepository(StudentInformationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task SaveUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

