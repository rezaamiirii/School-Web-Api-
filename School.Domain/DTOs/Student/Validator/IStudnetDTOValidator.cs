using FluentValidation;
using School.Domain.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student.Validator
{
    public class IStudnetDTOValidator:AbstractValidator<IStudentDTO>
    {
        public IStudnetDTOValidator()
        {

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required.").Length(3,50);
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required.").Length(3,50);
            RuleFor(x => x.NationalCode).NotEmpty().WithMessage("لطفا کد ملی را وارد کنید").Length(10).WithMessage("تعداد کاراکتر های کد ملی صحیح نیست");
            RuleFor(x => x.Birthday).Must(BeAValidDate).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.StudentPhoneNumber).NotEmpty().WithMessage("{PropertyName} is required.").Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");
           
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
