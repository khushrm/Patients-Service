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
        [HttpGet]
        public async Task<ICollection<PatientApiModel>> Get()
        {
            var patients = await _manager.GetPatients();

            return patients;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _manager.GetPatient(id);
            if(patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var patients = await _manager.GetPatient(name);
            
            return Ok(patients);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientApiModel patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var p = await _manager.AddPatient(patient);

            return Ok(p);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, PatientApiModel p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var patient = await _manager.EditPatient(id, p);

            return Ok(patient);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p =  await _manager.DeletePatient(id);

            return Ok(p);
        }
    }
}
