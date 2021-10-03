using Microsoft.EntityFrameworkCore;
using PatientService.Data.shared;
using PatientService.Domain.Entities;
using PatientService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Data.ReposiotryEF
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientsDbContext _context;
        public PatientRepository(PatientsDbContext context)
        {
            _context = context;
        }
        public async Task<Patient> GetPatient(int id)
        {
            return  await _context.Patients.FindAsync(id);
        }

        public async Task<ICollection<Patient>> GetPatients()
        {
            return await _context.Patients.Include("MedicalIssues").ToListAsync();
        }

        public void AddPatient(Patient patient)
        {
            patient.PId = "P-" + Pid.Id++;
            
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }
        public Patient EditPatient(int id, Patient patient)
        {
            var p = _context.Patients.Find(id);
            p = patient;

            _context.SaveChanges();

            return p;
        }

        public Patient DeletePatient(int id)
        {
            var p = _context.Patients.Find(id);

            _context.Patients.Remove(p);

            return p;
        }
    }
}
