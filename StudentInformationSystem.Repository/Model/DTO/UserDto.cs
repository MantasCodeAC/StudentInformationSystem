using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Model.DTO
{
    public class UserDto
    {
        [BindRequired]
        public string Username { get; set; } = string.Empty;
        [BindRequired]
        public string Password { get; set; } = string.Empty;
    }
}
