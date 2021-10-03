using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PatientService.Domain.Entities
{
    public class Patient
    {
        public Patient()
        {
            MedicalIssues = new List<MedicalIssue>();
        }
        [Key]
        public int Id { get; set; }
        public string PId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber{ get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public ICollection<MedicalIssue> MedicalIssues { get; set; }
    }
}
