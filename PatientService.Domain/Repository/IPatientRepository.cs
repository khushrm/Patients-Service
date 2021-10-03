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

        void AddPatient(Patient p);
        Patient EditPatient(int id, Patient p);
        Patient DeletePatient(int id);
        #endregion
    }
}
