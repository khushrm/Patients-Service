using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using PatientService.Domain.Manager;
using PatientService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IManager _manager;
        public PatientsController(IManager manager)
        {
            _manager = manager;
        }
        public async Task<ICollection<PatientApiModel>> Get()
        {
            var patients = await _manager.GetPatients();

            return patients;
        }
        [Route("{id}")]
        public async Task<PatientApiModel> Get(int id)
        {
            var patient = await _manager.GetPatient(id);

            return patient;
        }
        [HttpPost]
        public void Post([FromBody] PatientApiModel patient)
        {
            _manager.AddPatient(patient);
        }
        [HttpPut]
        [Route("{id}")]
        public PatientApiModel Put(int id, PatientApiModel p)
        {
            return _manager.EditPatient(id, p);
        }

        [HttpDelete]
        public PatientApiModel Delete(int id)
        {
            return _manager.DeletePatient(id);
        }
    }
}
