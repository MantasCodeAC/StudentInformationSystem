using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult Login([FromQuery] UserDto request)
        {
            var response = _userService.Login(request.Username, request.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost("Signup")]
        public ActionResult<ResponseDto> Signup([FromQuery] UserDto request)
        {
            var response = _userService.Signup(request.Username, request.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}
