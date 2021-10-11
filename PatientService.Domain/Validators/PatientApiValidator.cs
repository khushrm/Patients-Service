using FluentValidation;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientService.Domain.Validators
{
    public class PatientApiValidator : AbstractValidator<PatientApiModel>
    {
        public PatientApiValidator()
        {   
            RuleFor(p => p.Name).NotEmpty().MaximumLength(int.MaxValue);
            RuleFor(p => p.MobileNumber).NotEmpty().Length(10);
            RuleFor(p => p.Email).NotEmpty().MaximumLength(int.MaxValue);
            RuleFor(p => p.DateOfBirth).NotEmpty();
            RuleFor(p => p.BloodGroup).NotEmpty();
            RuleForEach(p => p.MedicalIssues)
                .SetValidator(new MedicalIssueApiValidator())
                .When(p => p.MedicalIssues != null);
        }
    }
}
