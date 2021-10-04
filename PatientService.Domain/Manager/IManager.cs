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
        Task<ICollection<PatientApiModel>> GetPatient(string name);
        Task<PatientApiModel> AddPatient(PatientApiModel p);
        Task<PatientApiModel> EditPatient(int id, PatientApiModel p);
        Task<PatientApiModel> DeletePatient(int id);
    }
}
