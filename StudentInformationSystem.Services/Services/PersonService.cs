using Microsoft.AspNetCore.Http;
using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using StudentInformationSystem.Repository.Repositories;
using StudentInformationSystem.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Microsoft.IdentityModel.Tokens;

namespace StudentInformationSystem.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PersonService(IPersonRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseDto> AddPersonAsync(string firstName, string lastName, int personalCode, string phoneNumber, string email, ImageUploadRequest imageUploadRequest, ResidenceDto residenceDto)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var existingPerson = await _repository.GetPersonAsync(personalCode);
            if (existingPerson is not null)
                return new ResponseDto(false, "Person already exists in a database");
            if (await CheckIfUserHasPersonAsync(currentUserId) is not null)
                return new ResponseDto(false, "This User's Personal information is already filled");
            Person person = CreatePerson(firstName, lastName, personalCode, phoneNumber, email, imageUploadRequest, residenceDto);
            person.UserId = currentUserId;
            await _repository.CreatePersonAsync(person);
            return new ResponseDto(true, $"{person.FirstName} {person.LastName} successfully added to database");
        }
        public async Task<ResponseDto> GetPersonInfoAsync(Guid personId)
        {
            Person existingPerson = await _repository.GetPersonAsync(personId);
            if (existingPerson is null)
                return new ResponseDto(false, "Person couldn't be found by this ID");
            Residence existingResidence = await _repository.GetResidenceAsync(personId);
            ProfilePicture existingProfilePicture = await _repository.GetProfilePictureByPersonAsync(personId);
            byte[] byteArray = existingProfilePicture.Data;
            return new ResponseDto(true, 
                $"Person Name: {existingPerson.FirstName}" + Environment.NewLine +
                $"Person LastName: {existingPerson.LastName}" + Environment.NewLine +
                $"Person Email: {existingPerson.Email}" + Environment.NewLine +
                $"PersonPhone: {existingPerson.PhoneNumber}" + Environment.NewLine +
                $"PersonalCode: {existingPerson.PersonalCode}" + Environment.NewLine +
                $"Residence Address: {existingResidence.City} {existingResidence.Street} " +
                $"{existingResidence.HouseNumber} {existingResidence.ApartmentNumber}" + Environment.NewLine +
                $"Profile Picture Data: {Convert.ToBase64String(byteArray, 0, byteArray.Length)}");
        }
        public async Task<ResponseDto> UpdatePersonNameAsync(string personName)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.FirstName = personName;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person FirstName changed to {personName}");
        }
        public async Task<ResponseDto> UpdatePersonLastNameAsync(string personLastName)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.LastName = personLastName;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person LastName changed to {personLastName}");
        }
        public async Task<ResponseDto> UpdatePersonalCodeAsync(int personalCode)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.PersonalCode = personalCode;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person PersonalCode changed to {personalCode}");
        }
        public async Task<ResponseDto> UpdatePersonPhoneAsync(string personPhone)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.PhoneNumber = personPhone;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person Phone Number changed to {personPhone}");
        }
        public async Task<ResponseDto> UpdatePersonEmailAsync(string personEmail)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.Email = personEmail;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person Email changed to {personEmail}");
        }
        public async Task<ResponseDto> UpdatePersonResidenceAsync(ResidenceDto residenceDto)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            var residence = await _repository.GetResidenceAsync(person.PersonID);
            residence.City = residenceDto.City;
            residence.Street = residenceDto.Street;
            residence.HouseNumber = residenceDto.HouseNumber;
            residence.ApartmentNumber = residenceDto.ApartmentNumber;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person residence address was changed");
        }
        public async Task<ResponseDto> UpdateProfilePictureAsync(ImageUploadRequest imageUploadRequest)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var person = await CheckIfUserHasPersonAsync(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            var profilePicture = await _repository.GetProfilePictureByPersonAsync(person.PersonID);
            var newPicture = CreateProfilePicture(imageUploadRequest);
            profilePicture.Data = newPicture.Data;
            profilePicture.Name = newPicture.Name;
            await _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person Profile Picture was changed");
        }

        private Person CreatePerson(string firstName, string lastName, int personalCode, string phoneNumber, string email, ImageUploadRequest imageUploadRequest, ResidenceDto residenceDto)
        {
 
            Person person = new Person
            {
                PersonID = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                PersonalCode = personalCode,
                PhoneNumber = phoneNumber,
                Email = email,
                ProfilePicture = CreateProfilePicture(imageUploadRequest),
                Residence = CreateResidence(residenceDto)
            };
            return person;
        }
        private ProfilePicture CreateProfilePicture(ImageUploadRequest imageUploadRequest)
        {
            using var stream = imageUploadRequest.Image.OpenReadStream();
            var image = Image.FromStream(stream);
            Image thumb = image.GetThumbnailImage(200, 200, () => false, IntPtr.Zero);           
            ProfilePicture profilePicture = new()
            {
                Id = Guid.NewGuid(),
                Name = imageUploadRequest.Image.FileName,
                Data = ConvertThumbToByte(thumb),
            };
            return profilePicture;
        }
        private Residence CreateResidence(ResidenceDto residenceDto)
        {
            Residence residence = new Residence
            {
                Id = Guid.NewGuid(),
                City = residenceDto.City,
                Street = residenceDto.Street,
                HouseNumber = residenceDto.HouseNumber,
                ApartmentNumber = residenceDto.ApartmentNumber
            };
            return residence;
        }
        private static byte[] ConvertThumbToByte(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
        private async Task<Person> CheckIfUserHasPersonAsync(Guid userId)
        {
            return await _repository.GetPersonByUserAsync(userId);
        }
    }
}
