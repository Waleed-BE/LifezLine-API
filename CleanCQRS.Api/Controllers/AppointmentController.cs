using CleanCQRS.Application.Command;
using CleanCQRS.Application.Query;
using CleanCQRS.Core.Dtos.Appointment;
using CleanCQRS.Core.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IMediator mediator, IOptions<ConnectionStringsOptions> options) : ControllerBase
    {
        [HttpPost("Add-Appointment")]
        public async Task<IActionResult> AddAppointment(AddAppointmentDto addAppointmentDto)
        {
            var result = await mediator.Send(new CreateAppointmentCommand(addAppointmentDto));
            return Ok(result);
        }

        [HttpGet("Get-Appointment-By-Patient-Id")]
        public async Task<IActionResult> GetAppointmentByPatientId(Guid PatientId)
        {
            var result = await mediator.Send(new GetAppointmentByPatientIdQuery(PatientId));
            return Ok(result);
        }


        [HttpGet("Get-Appointment-By-Doctor-Id")]
        public async Task<IActionResult> GetAppointmentByDoctorId(Guid DoctorId)
        {
            var result = await mediator.Send(new GetAppointmentByDoctorIdQuery(DoctorId));
            return Ok(result);
        }
    }
}
