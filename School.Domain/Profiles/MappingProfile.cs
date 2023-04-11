using AutoMapper;
using School.Domain.DTOs.Admin.Account;
using School.Domain.DTOs.Collage;
using School.Domain.DTOs.MapperDTO;
using School.Domain.DTOs.Student;
using School.Domain.Models.Account;
using School.Domain.Models.College;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, EditStudentProfileDTO>().ReverseMap();
            CreateMap<MainStudentRegister, MainStudentRegisterDTO>().ReverseMap();
            CreateMap<Student,FilteringStudentDto>().ReverseMap();
            CreateMap<Student, EditUserFromAdminDTO>().ReverseMap();
            CreateMap<College, CreateCollegeDTO>().ReverseMap();
            CreateMap<College, EditCollegeDTO>().ReverseMap();
            CreateMap<College, FilteringCollegeDTO>().ReverseMap();
        }
    }
}
