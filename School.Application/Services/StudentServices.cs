using School.Application.Interaces;
using School.Domain.Interaces;
using School.Domain.Models.Account;
using School.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Services
{
    public class StudentServices: IStudentServices
    {
        #region constructore
        private readonly IStudentRepository _studentRepository;
        private readonly ISmsService _smsService;
        private readonly IPasswordHelper _passwordHelper;

        public StudentServices(IStudentRepository studentRepository, IPasswordHelper passwordHelper, ISmsService smsService)
        {
            _studentRepository = studentRepository;
            _passwordHelper = passwordHelper;
            _smsService = smsService;
        }


        #endregion

        #region properties
        public async Task<RegisterStudentResult> RegisterStudent(RegisterStudentDto register)
        {
            if(! await _studentRepository.IsStudentExistByPhoneNumber(register.StudentPhoneNumber))
            {
               if(!await _studentRepository.IsStudentExistByNationalCode(register.NationalCode))
                {
                    var student = new Student()
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        NationalCode = register.NationalCode,
                        Birthday = register.Birthday,
                        IsStudent = false,
                        IsBlock = false,
                        IsDelete = false,
                        IsActive = false,
                        MobileActiveCode = new Random().Next(10000, 99999).ToString(),
                        Password = _passwordHelper.HashPassword(register.Password),
                        StudentPhoneNumber = register.StudentPhoneNumber,
                        SecondMajorPriority = register.SecondMajorPriority,
                        FirstMajorPriority = register.FirstMajorPriority,
                        ThirdMajorPriority = register.ThirdMajorPriority,
                        NameOfExSchool = register.NameOfExSchool,
                        AverageOfNineLevel = register.AverageOfNineLevel,
                        MarkOfMath = register.MarkOfMath,
                        MarkOfScience = register.MarkOfScience,
                        MarkOfWorkAndTechnology = register.MarkOfWorkAndTechnology,
                        
                    };

                    await _studentRepository.AddStudent(student);
                    await _studentRepository.SaveChanges();
                   // await _smsService.SendVerificationCode(student.StudentPhoneNumber,student.MobileActiveCode);
                    return RegisterStudentResult.Success;

                }
                return RegisterStudentResult.NatonaiCodeExists;
            }
            return RegisterStudentResult.PhoneNumberExists;
        }
        #endregion
    }
}
