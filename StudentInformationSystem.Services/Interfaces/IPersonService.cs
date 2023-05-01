using StudentInformationSystem.Repository.Model.DTO;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResponseDto> AddPersonAsync(string firstName, string lastName, int personalCode, string phoneNumber, string email, ImageUploadRequest imageUploadRequest, ResidenceDto residenceDto);
        Task<ResponseDto> UpdatePersonNameAsync(string personName);
        Task<ResponseDto> UpdatePersonLastNameAsync(string personLastName);
        Task<ResponseDto> UpdatePersonalCodeAsync(int personalCode);
        Task<ResponseDto> UpdatePersonPhoneAsync(string personPhone);
        Task<ResponseDto> UpdatePersonEmailAsync(string personEmail);
        Task<ResponseDto> UpdatePersonResidenceAsync(ResidenceDto residenceDto);
        Task<ResponseDto> UpdateProfilePictureAsync(ImageUploadRequest imageUploadRequest);
        Task<ResponseDto> GetPersonInfoAsync(Guid personId);
    }
}
