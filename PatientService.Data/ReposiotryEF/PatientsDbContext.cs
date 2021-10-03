using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientService.Data.ReposiotryEF
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalIssue> MedicalIssues { get; set; }
    }
}
