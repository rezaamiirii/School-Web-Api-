using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Fee.Validator
{
    public class PayFeeDTOValidator : AbstractValidator<PayFeeDTO>
    {
        public PayFeeDTOValidator()
        {
            RuleFor(x=>x.Amount).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
        }
    }
}
