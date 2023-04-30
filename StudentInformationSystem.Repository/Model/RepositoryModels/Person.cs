using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Model.RepositoryModels
{
    public class Person
    {
        public Guid PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonalCode { get; set; } 
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
        public Residence Residence { get; set; }
        public Guid UserId { get; set; }

    }
}
