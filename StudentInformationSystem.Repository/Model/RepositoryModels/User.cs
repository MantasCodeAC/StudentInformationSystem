using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Model.RepositoryModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [EnumDataType(typeof(Role))]
        public Role role { get; set; }
        public Person Person { get; set; }

        [DefaultValue(User)]
        public enum Role
        {
            User = 0,
            Admin = 1
        }
    }
}
