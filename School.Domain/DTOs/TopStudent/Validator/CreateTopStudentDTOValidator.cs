using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.TopStudent.Validator
{
    public class CreateTopStudentDTOValidator : AbstractValidator<CreateTopStudentDTO>
    {
        public CreateTopStudentDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
            RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
            RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
            RuleFor(x => x.Image).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

        }
    }
}
