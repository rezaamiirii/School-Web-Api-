using AutoMapper;
using School.Domain.DTOs.MapperDTO;
using School.Domain.DTOs.Student;
using School.Domain.Models.Account;
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

        }
    }
}
