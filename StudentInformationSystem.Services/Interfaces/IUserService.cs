using StudentInformationSystem.Repository.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto> SignupAsync(string username, string password);
        Task<ResponseDto> LoginAsync(string username, string password);
    }
}
