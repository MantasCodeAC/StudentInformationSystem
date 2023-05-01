using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminservice)
        {
            _adminService = adminservice;
        }
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<ResponseDto>> DeleteUserAsync([Required]Guid userIdToDelete)
        {
            var response = await _adminService.DeleteUserAsync(userIdToDelete);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}

