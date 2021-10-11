using FluentValidation;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientService.Domain.Validators
{
    public class MedicalIssueApiValidator : AbstractValidator<MedicalIssue>
    {
        public MedicalIssueApiValidator()
        {
            RuleFor(m => m.Id).NotEmpty().NotNull();
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Description).NotNull().Length(0, 200);
        }
    }
}
