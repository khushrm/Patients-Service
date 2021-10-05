using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _cache;

        public ManagerImpl(IPatientRepository repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }

        #region patients CRUD

        public async Task<PatientApiModel> AddPatient(PatientApiModel p)
        {
            try
            {
                var t = _mapper.Map<Patient>(p);
                t = await _repo.AddPatient(t);

                _cache.Set(string.Concat("Patient-", p.Id), t, DateTimeOffset.Now.AddSeconds(30));
                return _mapper.Map<PatientApiModel>(t);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<PatientApiModel> DeletePatient(int id)
        {
            try
            {
                var patient = await _repo.DeletePatient(id);

                if (_cache.Get(string.Concat("Patient-", id)) != null)
                {
                    _cache.Remove(string.Concat("Patient-", id));
                }

                return _mapper.Map<PatientApiModel>(patient);
            }

            catch(Exception e)
            {
                throw e;
            }

        }

        public async Task<PatientApiModel> EditPatient(int id, PatientApiModel p)
        {
            try
            {
                var t = _mapper.Map<Patient>(p);
                var patient = await _repo.EditPatient(id, t);

                _cache.Set(string.Concat("Patient-", id), patient, DateTimeOffset.Now.AddSeconds(30));

                return _mapper.Map<PatientApiModel>(patient);
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public async Task<PatientApiModel> GetPatient(int id)
        {
            try
            {
                if (_cache.Get("Patient-" + id) != null)
                {
                    return (PatientApiModel)_cache.Get("Patient-" + id);
                }
                var patient = await _repo.GetPatient(id);

                _cache.Set(string.Concat("Patient-", id), patient, DateTimeOffset.Now.AddSeconds(30));
                return _mapper.Map<PatientApiModel>(patient);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        public async Task<ICollection<PatientApiModel>> GetPatient(string name)
        {
            try
            {
                if (_cache.Get(string.Concat("Patients-", name)) != null)
                {
                    return _cache.Get(string.Concat("Patients-", name)) as ICollection<PatientApiModel>;
                }
                var patients = await _repo.GetPatient(name);

                var patientsapi = new List<PatientApiModel>();

                foreach (var p in patients)
                {
                    patientsapi.Add(_mapper.Map<PatientApiModel>(p));
                }

                _cache.Set(string.Concat("Patients-", name), patientsapi, DateTimeOffset.Now.AddSeconds(30));

                return patientsapi;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task<ICollection<PatientApiModel>> GetPatients()
        {
            try
            {
                if (_cache.Get("Patients") != null)
                {
                    return _cache.Get("Patients") as ICollection<PatientApiModel>;
                }
                var patients = await _repo.GetPatients();

                var patientsapi = new List<PatientApiModel>();

                foreach (var p in patients)
                {
                    patientsapi.Add(_mapper.Map<PatientApiModel>(p));
                }

                _cache.Set("Patients", patientsapi, DateTimeOffset.Now.AddSeconds(30));

                return patientsapi;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        #endregion

        #region Medical Issues
        public async Task<ICollection<MedicalIssuesApiModel>> AddMedicalIssueForPatient(int patientId, MedicalIssuesApiModel issue)
        {
            try
            {
                var medIssue = _mapper.Map<MedicalIssue>(issue);

                var medicalIssues = await _repo.AddMedicalIssueToPatient(patientId, medIssue);

                var issuesApiList = Convert(medicalIssues);

                return issuesApiList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ICollection<MedicalIssuesApiModel>> GetMedicalIssuesByPatientId(int patientId)
        {
            try
            {
                var medicalIssues = await _repo.GetMedicalIssuesByPatientId(patientId);

                var issuesApiList = Convert(medicalIssues);

                return issuesApiList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        private ICollection<MedicalIssuesApiModel> Convert(ICollection<MedicalIssue> medicalIssues)
        {
            try
            {
                var issuesApiList = new List<MedicalIssuesApiModel>();
                foreach (var i in medicalIssues)
                    issuesApiList.Add(_mapper.Map<MedicalIssuesApiModel>(i));

                return issuesApiList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
