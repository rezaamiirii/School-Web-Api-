using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student.Validator
{
    public class ActiveAccountDTOValidator : AbstractValidator<ActiveAccountDTO>
    {
        public ActiveAccountDTOValidator()
        {
            RuleFor(x=>x.ActiveCode).NotEmpty().WithMessage("{PropertyName} is required.").Length(1, 200);
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("{PropertyName} is required.").Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");
        }
    }
}
