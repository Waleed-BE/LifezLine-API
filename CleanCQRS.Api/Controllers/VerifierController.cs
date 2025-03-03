using CleanCQRS.Application.Command;
using CleanCQRS.Core.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifierController(IMediator mediator, IOptions<ConnectionStringsOptions> options) : ControllerBase
    {
        [HttpPost("Approve-Appointment-Receipt")]
        public async Task<IActionResult> ApproveAppointmentReceipt(Guid AppointmentId)
        {
            var result = await mediator.Send(new CreateAppointmentVerificationCommand(AppointmentId));
            return Ok(result);
        }
    }
}
