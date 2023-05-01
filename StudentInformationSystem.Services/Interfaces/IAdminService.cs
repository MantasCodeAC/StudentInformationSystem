using StudentInformationSystem.Repository.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ResponseDto> DeleteUserAsync(Guid userIdToDelete);
    }
}
