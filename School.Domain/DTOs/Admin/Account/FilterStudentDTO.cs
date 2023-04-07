using School.Domain.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Admin.Account
{
    public class FilterStudentDTO: RequestParams
    {

        public string LastName { get; set; } = "";
        
        public StudentState  StudentState { get; set; }

        
    }
    public enum StudentState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال ")]
        IsActive,
        [Display(Name = " حذف شده ")]
        Delete,
        [Display(Name = "جدیدترین ها")]
        StudentNews,
        [Display(Name = "دانش آموز های مدرسه")]
        MainStudent,


    }
}
