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
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string MobileNumber{ get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        public ICollection<MedicalIssue> MedicalIssues { get; set; }

        //Fluent API 
    }
}
