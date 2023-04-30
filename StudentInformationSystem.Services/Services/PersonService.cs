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
        public ResponseDto AddPerson(string firstName, string lastName, int personalCode, string phoneNumber, string email, ImageUploadRequest imageUploadRequest, ResidenceDto residenceDto)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            var existingPerson = _repository.GetPersonAsync(personalCode);
            if (existingPerson is not null)
                return new ResponseDto(false, "Person already exists in a database");
            if (CheckIfUserHasPerson(currentUserId) is not null)
                return new ResponseDto(false, "This User's Personal information is already filled");
            Person person = CreatePerson(firstName, lastName, personalCode, phoneNumber, email, imageUploadRequest, residenceDto);
            person.UserId = currentUserId;
            _repository.CreatePersonAsync(person);
            return new ResponseDto(true, $"{person.FirstName} {person.LastName} successfully added to database");
        }
        public ResponseDto GetPersonInfo(Guid personId)
        {
            Person existingPerson = _repository.GetPersonAsync(personId);
            if (existingPerson is null)
                return new ResponseDto(false, "Person couldn't be found by this ID");
            Residence existingResidence = _repository.GetResidenceAsync(personId);
            ProfilePicture existingProfilePicture = _repository.GetProfilePictureByPersonAsync(personId);
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
        public ResponseDto UpdatePersonName(string personName)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.FirstName = personName;
            _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person FirstName changed to {personName}");
        }
        public ResponseDto UpdatePersonLastName(string personLastName)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.LastName = personLastName;
            _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person LastName changed to {personLastName}");
        }
        public ResponseDto UpdatePersonalCode(int personalCode)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.PersonalCode = personalCode;
            _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person PersonalCode changed to {personalCode}");
        }
        public ResponseDto UpdatePersonPhone(string personPhone)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.PhoneNumber = personPhone;
            _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person Phone Number changed to {personPhone}");
        }
        public ResponseDto UpdatePersonEmail(string personEmail)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            person.Email = personEmail;
            _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person Email changed to {personEmail}");
        }
        public ResponseDto UpdatePersonResidence(ResidenceDto residenceDto)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            Residence residence = _repository.GetResidenceAsync(person.PersonID);
            residence.City = residenceDto.City;
            residence.Street = residenceDto.Street;
            residence.HouseNumber = residenceDto.HouseNumber;
            residence.ApartmentNumber = residenceDto.ApartmentNumber;
            _repository.UpdatePersonAsync();
            return new ResponseDto(true, $"Person residence address was changed");
        }
        public ResponseDto UpdateProfilePicture(ImageUploadRequest imageUploadRequest)
        {
            var currentUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            Person person = CheckIfUserHasPerson(currentUserId);
            if (person is null)
                return new ResponseDto(false, "This User's Personal information is empty");
            ProfilePicture profilePicture = _repository.GetProfilePictureByPersonAsync(person.PersonID);
            var newPicture = CreateProfilePicture(imageUploadRequest);
            profilePicture.Data = newPicture.Data;
            profilePicture.Name = newPicture.Name;
            _repository.UpdatePersonAsync();
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
        private Person CheckIfUserHasPerson(Guid userId)
        {
            return _repository.GetPersonByUserAsync(userId);
        }
    }
}
