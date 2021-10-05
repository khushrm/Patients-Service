using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalIssuesController : ControllerBase
    {
        private readonly IManager _manager;
        public MedicalIssuesController(IManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        [Route("{patientId}")]
        public async Task<IActionResult> GetIssuesById(int patientId)
        {
            var issues = await _manager.GetMedicalIssuesByPatientId(patientId);

            if (issues == null)
                return BadRequest();

            return Ok(issues);
        }
        [HttpPost]
        [Route("{patientId}")]
        public async Task<IActionResult> PostMedicalIssueForPatient(int patientId, [FromBody] MedicalIssuesApiModel issue)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var issues = await _manager.AddMedicalIssueForPatient(patientId, issue);

            return Ok(issues);

        }
    }
}
