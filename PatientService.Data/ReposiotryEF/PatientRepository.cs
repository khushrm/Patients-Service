using Microsoft.EntityFrameworkCore;
using PatientService.Data.shared;
using PatientService.Domain.Entities;
using PatientService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// returns Patient by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Patient> GetPatient(int id)
        {
            return  await _context.Patients.FindAsync(id);
        }
        /// <summary>
        /// list of patients , param is substr in patient name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<Patient>> GetPatient(string name)
        {
            return await _context.Patients.Where(patient => patient.Name.Contains(name)).ToListAsync();
        }

        /// <summary>
        /// returns all the patients details
        /// </summary>
        /// <returns></returns>

        public async Task<ICollection<Patient>> GetPatients()
        {
            return await _context.Patients.Include("MedicalIssues").ToListAsync();
        }

        /// <summary>
        /// adds a patient to db
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task<Patient> AddPatient(Patient patient)
        {
            patient.PId = "P-" + Pid.Id++;
            
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }
        /// <summary>
        /// edit patient details with id and patient object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public async Task<Patient> EditPatient(int id, Patient p)
        {
            var t = await _context.Patients.FindAsync(id);
            t.Name = p.Name;
            t.MobileNumber = p.MobileNumber;
            t.Email = p.Email;
            t.DateOfBirth = p.DateOfBirth;
            t.BloodGroup = p.BloodGroup;
            t.MedicalIssues = p.MedicalIssues;

           await _context.SaveChangesAsync();

            return t;
        }

        /// <summary>
        /// Removes the patient from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<Patient> DeletePatient(int id)
        {
            var p = await _context.Patients.FindAsync(id);

             _context.Patients.Remove(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public async Task<ICollection<MedicalIssue>> GetMedicalIssuesByPatientId(int patientId)
        {
            var patient = await _context.Patients
                .Include("MedicalIssues")
                .Where(x => x.Id == patientId)
                .FirstOrDefaultAsync();

            if (patient == null)
                return null;

            return patient.MedicalIssues;

        }

        public async Task<ICollection<MedicalIssue>> AddMedicalIssueToPatient(int patientId,MedicalIssue issue)
        {
            var patient = await _context.Patients
                .Include("MedicalIssues")
                .Where(x => x.Id == patientId)
                .FirstOrDefaultAsync();

            if (patient == null)
                return null;

            patient.MedicalIssues.Add(issue);

            await _context.SaveChangesAsync();

            return patient.MedicalIssues;
        }
    }
}
