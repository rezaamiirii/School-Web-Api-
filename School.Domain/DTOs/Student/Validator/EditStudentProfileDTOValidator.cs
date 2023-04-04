using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.Student.Validator
{
    public class EditStudentProfileDTOValidator : AbstractValidator<EditStudentProfileDTO>
    {
        public EditStudentProfileDTOValidator()
        {
            Include(new IStudnetDTOValidator());
            RuleFor(x => x.AverageOfNineLevel).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.NameOfExSchool).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.MarkOfMath).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.MarkOfScience).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.MarkOfWorkAndTechnology).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.FirstMajorPriority).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.SecondMajorPriority).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.ThirdMajorPriority).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
