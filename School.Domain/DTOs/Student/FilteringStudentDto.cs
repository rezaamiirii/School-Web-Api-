using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student
{
    public class FilteringStudentDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public DateTime Birthday { get; set; }
        public string? StudentPhoneNumber { get; set; }

        public decimal AverageOfNineLevel { get; set; }
        public decimal MarkOfMath { get; set; }
        public decimal MarkOfScience { get; set; }
        public decimal MarkOfWorkAndTechnology { get; set; }
        public string? NameOfExSchool { get; set; }

        public string? FirstMajorPriority { get; set; }
        public string? SecondMajorPriority { get; set; }
        public string? ThirdMajorPriority { get; set; }
    }
}
