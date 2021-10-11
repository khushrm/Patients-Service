using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Manager;
using PatientService.Domain.Validators;
using System;
using System.Threading.Tasks;

namespace PatientService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IManager _manager;
        //private ILogger _logger = (ILogger)LogManager.GetLogger("fileLogger");
        private ILogger _logger;
        public PatientsController(IManager manager, ILogger logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var patients = await _manager.GetPatients();

                return Ok(patients);
            }
            catch(Exception e)
            {
                // log it
                _logger.LogError(e.Message);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
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
                _logger.LogError(e.Message);
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
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientApiModel patient)
        {
            try
            {
                var validator = new PatientApiValidator();
                var result = validator.Validate(patient);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                var p = await _manager.AddPatient(patient);

                return Ok(p);
            }
            catch(Exception e)
            {
                // log it
                _logger.LogError(e.Message);
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
                _logger.LogError(e.Message);
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
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            
        }
    }
}
