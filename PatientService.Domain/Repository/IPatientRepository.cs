using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Repository
{
    public interface IPatientRepository
    {
        #region async methods
        Task<ICollection<Patient>> GetPatients();
        Task<Patient> GetPatient(int id);
        Task<List<Patient>> GetPatient(string name);

        Task<Patient> AddPatient(Patient p);
        Task<Patient> EditPatient(int id, Patient p);
        Task<Patient> DeletePatient(int id);
        #endregion
    }
}
