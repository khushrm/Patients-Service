using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Manager
{
    public interface IManager
    {
        Task<ICollection<PatientApiModel>> GetPatients();
        Task<PatientApiModel> GetPatient(int id);

        void AddPatient(PatientApiModel p);
        PatientApiModel EditPatient(int id, PatientApiModel p);
        PatientApiModel DeletePatient(int id);
    }
}
