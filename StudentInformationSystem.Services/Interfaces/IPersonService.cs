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
        ResponseDto AddPerson(string firstName, string lastName, int personalCode, string phoneNumber, string email, ImageUploadRequest imageUploadRequest, ResidenceDto residenceDto);
        ResponseDto UpdatePersonName(string personName);
        ResponseDto UpdatePersonLastName(string personLastName);
        ResponseDto UpdatePersonalCode(int personalCode);
        ResponseDto UpdatePersonPhone(string personPhone);
        ResponseDto UpdatePersonEmail(string personEmail);
        ResponseDto UpdatePersonResidence(ResidenceDto residenceDto);
        ResponseDto UpdateProfilePicture(ImageUploadRequest imageUploadRequest);
        ResponseDto GetPersonInfo(Guid personId);
    }
}
