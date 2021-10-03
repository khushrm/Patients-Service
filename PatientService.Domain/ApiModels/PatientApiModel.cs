using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientService.Domain.ApiModels
{
    public class PatientApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public List<MedicalIssue> MedicalIssues { get; set; }
    }
}
