using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.Account
{
    public class Student : BaseEntity
    {

        #region Studenet's Info
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public DateTime Birthday { get; set; }

        public string? StudentPhoneNumber { get; set; }
        public string? FatherPhoneNumber { get; set; }
        public string? MotherPhoneNumber { get; set; }
        public string? MobileActiveCode { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlock { get; set; }
        public bool IsStudent { get; set; }
        public bool IsDelete { get; set; }

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

        #region relations

        public MainStudentRegister?   MainStudentRegister { get; set; }

        #endregion


    }

    public enum StateOfExSchool
    {
      First,
      Second,
      Third,
      Fourth,
      Other
    }


}

