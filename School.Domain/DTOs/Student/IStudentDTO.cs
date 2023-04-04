using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student
{
    public class IStudentDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public DateTime Birthday { get; set; }
        public string? StudentPhoneNumber { get; set; }
    }
}
