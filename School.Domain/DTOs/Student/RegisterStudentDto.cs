using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student
{
    public class RegisterStudentDto:IStudentDTO
    {
        #region student
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Token { get; set; }
        #endregion

        #region pre-registration

        public decimal AverageOfNineLevel { get; set; }
        public decimal MarkOfMath { get; set; }
        public decimal MarkOfScience { get; set; }
        public decimal MarkOfWorkAndTechnology { get; set; }
        public string? NameOfExSchool { get; set; }

        public string? FirstMajorPriority { get; set; }
        public string? SecondMajorPriority { get; set; }
        public string? ThirdMajorPriority { get; set; }

        
        #endregion
    }
    public enum RegisterStudentResult
    {
        NatonaiCodeExists,
        PhoneNumberExists,
        Success
    }
}
