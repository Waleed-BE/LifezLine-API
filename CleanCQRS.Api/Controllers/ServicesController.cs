using CleanCQRS.Application.Command;
using CleanCQRS.Application.Query;
using CleanCQRS.Core.Dtos.Services;
using CleanCQRS.Core.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController(IMediator mediator, IOptions<ConnectionStringsOptions> options) : ControllerBase
    {
        [HttpGet("Get-All-Provided-Services")]
        public async Task<IActionResult> GetAllProvidedServices()
        {
            var result = await mediator.Send(new GetAllProvidedServicesQuery());
            return Ok(result);
        }

        [HttpPost("Add-Edit-Service")]
        public async Task<IActionResult> AddEditProvidedServices(ServiceDTO serviceDTO)
        {
            var result = await mediator.Send(new CreateServiceCommand(serviceDTO));
            return Ok(result);
        }

    }
}
