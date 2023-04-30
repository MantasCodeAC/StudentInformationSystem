using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Model.DTO
{
    public class ResidenceDto
    {
        [BindRequired]
        public string City { get; set; }
        [BindRequired]
        public string Street { get; set; }
        [BindRequired]
        public int HouseNumber { get; set; }
        [BindRequired]
        public int ApartmentNumber { get; set; }
    }
}
