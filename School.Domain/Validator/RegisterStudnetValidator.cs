using FluentValidation;
using School.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Validator
{
    public class RegisterStudnetValidator:AbstractValidator<RegisterStudentDto>
    {
        public RegisterStudnetValidator()
        {

            RuleFor(x => x.FirstName).NotEmpty().Length(3,50);
            RuleFor(x => x.LastName).NotEmpty().Length(3,50);
            RuleFor(x => x.NationalCode).NotEmpty().WithMessage("لطفا کد ملی را وارد کنید").Length(10).WithMessage("تعداد کاراکتر های کد ملی صحیح نیست");
            RuleFor(x => x.Birthday).Must(BeAValidDate).NotEmpty();
            RuleFor(x => x.StudentPhoneNumber).NotEmpty().Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");
            RuleFor(x => x.MobileActiveCode).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().Length(6,200);
            RuleFor(x=>x.ConfirmPassword).NotEmpty().Length(6, 200).Equal(x => x.Password);
            RuleFor(x => x.AverageOfNineLevel).NotEmpty();

            RuleFor(x => x.MarkOfMath).NotEmpty();
            RuleFor(x => x.MarkOfScience).NotEmpty();
            RuleFor(x => x.MarkOfWorkAndTechnology).NotEmpty();
            RuleFor(x => x.FirstMajorPriority).NotEmpty();
            RuleFor(x => x.SecondMajorPriority).NotEmpty();
            RuleFor(x => x.ThirdMajorPriority).NotEmpty();
            RuleFor(x=>x.Token).NotEmpty();

        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
