using AutoMapper;
using School.Application.Interaces;
using School.Domain.DTOs.Admin.Account;
using School.Domain.DTOs.Student;
using School.Domain.Interaces;
using School.Domain.Models.Account;
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
        private readonly IMapper  _mapper;

        public StudentServices(IStudentRepository studentRepository, IPasswordHelper passwordHelper, ISmsService smsService, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _passwordHelper = passwordHelper;
            _smsService = smsService;
            _mapper = mapper;
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
                    await _smsService.SendVerificationCode(student.StudentPhoneNumber,student.MobileActiveCode);
                    return RegisterStudentResult.Success;

                }
                return RegisterStudentResult.NatonaiCodeExists;
            }
            return RegisterStudentResult.PhoneNumberExists;
        }



        public async Task<LoginStudentResult> LoginStudent(LoginDTO loginStudent)
        {
            var user = await _studentRepository.GetStudentByPhoneNumber(loginStudent.StudentPhoneNumber);

            if (user != null)
            {
                if (user.IsActive)
                {
                    if (!user.IsBlock)
                    {
                        if (_passwordHelper.HashPassword(loginStudent.Password) == user.Password)
                        {
                            return LoginStudentResult.Success;
                        }
                        return LoginStudentResult.NotFound;
                    }
                    return LoginStudentResult.IsBlocked;
                }
                return LoginStudentResult.NotActive;
            }
            return LoginStudentResult.NotFound;
        }

        public async Task<Student> GetStudentByPhoneNumber(string number)
        {
           return await _studentRepository.GetStudentByPhoneNumber(number);
        }

      

        public async Task<Student> GetStudentById(int studentId)
        {
            return await _studentRepository.GetStudentById(studentId);
        }

        public async Task<ActiveAccountResult> ActiveAccount(ActiveAccountDTO activeAccount)
        {
            var student = await _studentRepository.GetStudentByPhoneNumber(activeAccount.PhoneNumber);
            if (student == null) return ActiveAccountResult.NotFound;
            if (student.MobileActiveCode != activeAccount.ActiveCode) return ActiveAccountResult.Error;

            student.MobileActiveCode = new Random().Next(10000, 99999).ToString();
            student.IsActive = true;
            _studentRepository.UpdateStudent(student);
            await _studentRepository.SaveChanges();
            return ActiveAccountResult.Success;
        }

        public async Task<forgetPassResult> ForgetPass(ForgetPasswordDTO forgetPassword)
        {
            var student = await _studentRepository.GetStudentByPhoneNumber(forgetPassword.PhoneNumber);
            if (student == null) return forgetPassResult.NotFound;
            await _smsService.SendVerificationCode(student.StudentPhoneNumber, student.MobileActiveCode);
            return forgetPassResult.Success;
        }

        public async Task<EditStudentProfileDTO> GetEditStudentProfile(int studentId)
        {
            var student = await _studentRepository.GetStudentById(studentId);
            if (student != null)
            {
                //var editstudent = new EditStudentProfileDTO
                //{
                //    FirstName = student.FirstName,
                //    LastName = student.LastName,
                //    StudentPhoneNumber = student.StudentPhoneNumber,
                //   ,
                //};
                
                return _mapper.Map<EditStudentProfileDTO>(student); 
            }
            return null;
        }

        public async Task<EditStudentProfileResult> EditProfile(int studentId, EditStudentProfileDTO editUserProfile)
        {
            var student = await _studentRepository.GetStudentById(studentId);
            if (student == null) return EditStudentProfileResult.NotFound;

            var result = _mapper.Map(editUserProfile, student);

            _studentRepository.UpdateStudent(result);
            await _studentRepository.SaveChanges();
            return EditStudentProfileResult.Success;
        }
        public async Task<Student> GetStudentByMobileActiveCode(string mobileActiveCode)
        {
            return await _studentRepository.GetStudentByMobileActiveCode(mobileActiveCode);
        }

        public async Task<resetPasswordResult> ResettPasswordFromForget(ResetPasswordDTO resetPassword, string mobileActiveCode)
        {
            var student = await _studentRepository.GetStudentByMobileActiveCode(mobileActiveCode);
            if (student == null) return resetPasswordResult.Error;

            student.Password = _passwordHelper.HashPassword(resetPassword.Password);
            student.MobileActiveCode = new Random().Next(10000, 99999).ToString();
            student.IsActive = true;
            _studentRepository.UpdateStudent(student);
            await _studentRepository.SaveChanges();
            return resetPasswordResult.Success;
        }

        public async Task<IList<FilteringStudentDto>> FilterStudnets(FilterStudentDTO filter)
        {
            var students= await _studentRepository.FilterStudents(filter);
           var result=  _mapper.Map<IList<FilteringStudentDto>>(students);
            return result;
        }

        public async Task<EditUserFromAdminDTO> GetEditStudentFromAdmin(int studentId)
        {
           return await _studentRepository.GetEditStudentFromAdmin(studentId);
        }

        public async Task<EditStudentFromAdminResult> EditStudentFromAdmin(EditUserFromAdminDTO editUser)
        {
            var student = await _studentRepository.GetStudentById(editUser.Id);
            if (student == null) return EditStudentFromAdminResult.NotFound;

           var result= _mapper.Map(editUser, student);

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                result.Password = _passwordHelper.HashPassword(editUser.Password);
            }
            _studentRepository.UpdateStudent(result);
           
            await _studentRepository.SaveChanges();

            return EditStudentFromAdminResult.Success;
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            return await _studentRepository.DeleteStudent(studentId);
        }

        public async Task<bool> RecoverStudent(int studentId)
        {
            return await _studentRepository.RecoverStudent(studentId);
        }

        public async Task<bool> ChangeToStudent(int studentId)
        {
            return await _studentRepository.ChangeToStudent(studentId);
        }

        public async Task<bool> ChangeToSubStudent(int studentId)
        {
            return await _studentRepository.ChangeToSubStudent(studentId);
        }

      







        #endregion
    }
}
