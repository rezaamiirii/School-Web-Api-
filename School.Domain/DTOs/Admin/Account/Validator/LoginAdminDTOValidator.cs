using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Admin.Account.Validator
{
    public class LoginAdminDTOValidator : AbstractValidator<LoginAdminDTO>
    {
        public LoginAdminDTOValidator()
        {
            RuleFor(x => x.AdminPhoneNumber).NotEmpty().WithMessage("{PropertyName} is required.").Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} is required.").Length(6, 200);

        }
    }
}
