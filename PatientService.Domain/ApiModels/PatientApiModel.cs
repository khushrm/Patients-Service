using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PatientService.Domain.ApiModels
{
    public class PatientApiModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        public List<MedicalIssue> MedicalIssues { get; set; }
    }
}
