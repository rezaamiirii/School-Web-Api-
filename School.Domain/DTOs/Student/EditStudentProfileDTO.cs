using System.ComponentModel.DataAnnotations;

namespace School.Domain.DTOs.Student
{
    public class EditStudentProfileDTO:IStudentDTO
    {
      

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
    public enum EditStudentProfileResult
    {
        NotFound,
        Success
    }
}
