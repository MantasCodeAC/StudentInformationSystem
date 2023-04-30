using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudentInformationSystem.Repository.Model.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Model.DTO
{
    public class PersonDto
    {
        [BindRequired]
        public string FirstName { get; set; }
        [BindRequired]
        public string LastName { get; set; }
        [BindRequired]
        public int PersonalCode { get; set; }
        [BindRequired]
        public string PhoneNumber { get; set; }
        [BindRequired]
        public string Email { get; set; }
        public ImageUploadRequest ImageUploadRequest { get; set; }
        public ResidenceDto ResidenceDto { get; set; }
    }
}
