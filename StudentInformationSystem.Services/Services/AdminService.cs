using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Services.Services
{
    public class AdminService: IAdminService
    {
        private readonly IUserRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseDto DeleteUser(Guid userIdToDelete)
        {           
            User currentUser = _repository.GetUserAsync(Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId()));
            if (currentUser.role == 0)
                return new ResponseDto(false, "This User doesn't have Admin rights");
            User userToDelete = _repository.GetUserAsync(userIdToDelete);
            if (userToDelete is null)
            {
                return new ResponseDto(false, "User to delete ID doesn't exist");
            }
            _repository.DeleteUserAsync(userToDelete);
            return new ResponseDto(true, $"User {userToDelete.Username} and all related information successfully removed");
        }
    }
}
