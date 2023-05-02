using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace StudentInformationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpPost("CreatePerson")]
        public async Task<ActionResult<ResponseDto>> CreatePersonAsync([FromForm] PersonDto personDto)
        {
            var response = await _personService.AddPersonAsync(personDto.FirstName, personDto.LastName, personDto.PersonalCode,
                personDto.PhoneNumber, personDto.Email, personDto.ImageUploadRequest, personDto.ResidenceDto);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;           
        }
        [HttpGet("GetPersonInfoById")]
        public async Task<ActionResult<string>> GetPersonInfoAsync([Required] Guid personId)
        {
            var response = await _personService.GetPersonInfoAsync(personId);             
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response.Message;
        }
        [HttpPut("ModifyPersonName")]
        public async Task<ActionResult<ResponseDto>> ModifyNameAsync([Required] string newName)
        {
            var response = await _personService.UpdatePersonNameAsync(newName);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonLastName")]
        public async Task<ActionResult<ResponseDto>> ModifyLastNameAsync([Required] string newLastName)
        {
            var response = await _personService.UpdatePersonLastNameAsync(newLastName);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonalCode")]
        public async Task<ActionResult<ResponseDto>> ModifyPersonalCodeAsync([Required] int personalCode)
        {
            var response = await _personService.UpdatePersonalCodeAsync(personalCode);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonEmail")]
        public async Task<ActionResult<ResponseDto>> ModifyEmailAsync([Required] string email)
        {
            var response = await _personService.UpdatePersonEmailAsync(email);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonPhone")]
        public async Task<ActionResult<ResponseDto>> ModifyPhoneAsync([Required] string phone)
        {
            var response = await _personService.UpdatePersonPhoneAsync(phone);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonResidenceAddress")]
        public async Task<ActionResult<ResponseDto>> ModifyPersonResidenceAddressAsync([FromQuery]ResidenceDto newResidence)
        {
            var response = await _personService.UpdatePersonResidenceAsync(newResidence);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonProfilePicture")]
        public async Task<ActionResult<ResponseDto>> ModifyPersonProfilePictureAsync([FromForm] ImageUploadRequest imageUploadRequest)
        {
            var response = await _personService.UpdateProfilePictureAsync(imageUploadRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}
