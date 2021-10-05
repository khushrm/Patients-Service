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
        public async Task<IActionResult> Get()
        {
            try
            {
                var patients = await _manager.GetPatients();

                return Ok(patients);
            }
            catch(Exception e)
            {
                // log it

                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var patient = await _manager.GetPatient(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch(Exception e)
            {
                //log it

                return NotFound();
            }
        }
        [HttpGet]
        [Route("search/{name}")]
        public async Task<IActionResult> GetPatientsByName(string name)
        {
            try
            {
                var patients = await _manager.GetPatient(name);

                return Ok(patients);
            }
            catch(Exception e)
            {
                // log it

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientApiModel patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var p = await _manager.AddPatient(patient);

                return Ok(p);
            }
            catch(Exception e)
            {
                // log it

                return BadRequest();
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, PatientApiModel p)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var patient = await _manager.EditPatient(id, p);

                return Ok(patient);
            }
            catch(Exception e)
            {
                // log it

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var p = await _manager.DeletePatient(id);
                return Ok(p);
            }
            catch(Exception e)
            {
                // log it

                return BadRequest(e.Message);
            }
            
        }
    }
}
