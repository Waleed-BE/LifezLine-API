using CleanCQRS.Application.Command;
using CleanCQRS.Core.Dtos.Patient;
using CleanCQRS.Core.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IMediator mediator, IOptions<ConnectionStringsOptions> options) : ControllerBase
    {
        [HttpPost("Add-Edit-Patient")]
        public async Task<IActionResult> AddEditPatient(AddPatientDto patientDto)
        {
            var result = await mediator.Send(new CreatePatientCommand(patientDto));
            return Ok(result);
        }
    }
}
