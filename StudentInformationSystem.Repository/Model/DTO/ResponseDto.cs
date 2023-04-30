using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository.Model.DTO
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ResponseDto(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ResponseDto(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
