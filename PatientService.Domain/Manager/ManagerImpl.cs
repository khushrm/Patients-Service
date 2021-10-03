using AutoMapper;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using PatientService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Manager
{
    public class ManagerImpl : IManager
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;
        public ManagerImpl(IPatientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public void AddPatient(PatientApiModel p)
        {
            var t = new Patient { Name = p.Name, Email = p.Email, BloodGroup = p.BloodGroup, DateOfBirth = p.DateOfBirth, MobileNumber = p.MobileNumber };
            _repo.AddPatient(t);
        }

        public PatientApiModel DeletePatient(int id)
        {
            var patient = _repo.DeletePatient(id);

            return _mapper.Map<PatientApiModel>(patient);

        }

        public PatientApiModel EditPatient(int id, PatientApiModel p)
        {
            var t = _repo.GetPatient(id).Result;
            t.Name = p.Name;
            t.MobileNumber = p.MobileNumber;
            t.Email = p.Email;
            t.DateOfBirth = p.DateOfBirth;
            t.BloodGroup = p.BloodGroup;
            t.MedicalIssues = p.MedicalIssues;
            var patient = _repo.EditPatient(id, t);

            return _mapper.Map<PatientApiModel>(patient);
        }

        public async Task<PatientApiModel> GetPatient(int id)
        {
            var patient = await _repo.GetPatient(id);

            return _mapper.Map<PatientApiModel>(patient);
        }

        public async Task<ICollection<PatientApiModel>> GetPatients()
        {
            var patients = await _repo.GetPatients();

            var patientsapi = new List<PatientApiModel>();

            foreach(var p in patients)
            {
                patientsapi.Add(_mapper.Map<PatientApiModel>(p));
            }

            return patientsapi;
        }
    }
}
