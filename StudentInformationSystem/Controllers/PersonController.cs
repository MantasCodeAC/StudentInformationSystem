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
        public ActionResult<ResponseDto> CreatePerson([FromForm] PersonDto personDto)
        {
            var response = _personService.AddPerson(personDto.FirstName, personDto.LastName, personDto.PersonalCode,
                personDto.PhoneNumber, personDto.Email, personDto.ImageUploadRequest, personDto.ResidenceDto);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;           
        }
        [HttpGet("GetPersonInfoById")]
        public ActionResult<string> GetPersonInfo([Required] Guid personId)
        {
            var response = _personService.GetPersonInfo(personId);             
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response.Message;
        }
        [HttpPut("ModifyPersonName")]
        public ActionResult<ResponseDto> ModifyName([Required] string newName)
        {
            var response = _personService.UpdatePersonName(newName);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonLastName")]
        public ActionResult<ResponseDto> ModifyLastName([Required] string newLastName)
        {
            var response = _personService.UpdatePersonLastName(newLastName);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonalCode")]
        public ActionResult<ResponseDto> ModifyPersonalCode([Required] int personalCode)
        {
            var response = _personService.UpdatePersonalCode(personalCode);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonEmail")]
        public ActionResult<ResponseDto> ModifyEmail([Required] string email)
        {
            var response = _personService.UpdatePersonEmail(email);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonPhone")]
        public ActionResult<ResponseDto> ModifyPhone([Required] string phone)
        {
            var response = _personService.UpdatePersonPhone(phone);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonResidenceAddress")]
        public ActionResult<ResponseDto> ModifyPersonResidenceAddress([FromQuery]ResidenceDto newResidence)
        {
            var response = _personService.UpdatePersonResidence(newResidence);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPut("ModifyPersonProfilePicture")]
        public ActionResult<ResponseDto> ModifyPersonProfilePicture([FromForm] ImageUploadRequest imageUploadRequest)
        {
            var response = _personService.UpdateProfilePicture(imageUploadRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }

    }
}
