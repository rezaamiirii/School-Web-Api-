using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student.Validator
{
    public class ResetPasswordDTOValidator : AbstractValidator<ResetPasswordDTO>
    {
        public ResetPasswordDTOValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} is required.").Length(6, 200);
            RuleFor(x => x.MobileActiveCode).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("{PropertyName} is required.").Length(6, 200).Equal(x => x.Password);

        }
    }
}
