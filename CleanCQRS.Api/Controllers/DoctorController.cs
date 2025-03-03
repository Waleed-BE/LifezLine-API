using CleanCQRS.Application.Command;
using CleanCQRS.Application.Query;
using CleanCQRS.Core.Dtos.Doctor;
using CleanCQRS.Core.Dtos.DoctorService;
using CleanCQRS.Core.Dtos.OTP;
using CleanCQRS.Core.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(IMediator mediator, IOptions<ConnectionStringsOptions> options) : ControllerBase
    {
        [HttpPost("Add-Edit-Doctor")]
        public async Task<IActionResult> AddDoctor(AddDoctorDto doctorDto)
        {
            var result = await mediator.Send(new CreateDoctorCommand(doctorDto));
            return Ok(result);
        }

        [HttpGet("Expert-Panel")]
        public async Task<List<DoctorWithServicesDTO>> ExpertPanel()
        {
            return await mediator.Send(new GetDoctorServicesQuery());
        }

        [HttpPost("Add-Schedule")]
        public async Task<DoctorAvailabilityResponseDTO> AddDoctorSchedule(List<DoctorAvailabilityDTO> doctorAvailabilityDTOList)
        {
            return await mediator.Send(new CreateDoctorScheduleCommand(doctorAvailabilityDTOList));
        }

        [HttpGet("Get-Doctor-Schedule-By-Id")]
        public async Task<List<DoctorAvailabilityDTO>> GetDoctorScheduleById(Guid DoctorId)
        {
            return await mediator.Send(new GetDoctorScheduleQuery(DoctorId));
        }

        [HttpGet("Get-all-Doctors-Schedule-By-Date-Range")]
        public async Task<List<DoctorAvailabilityDTO>> GetAllDoctorScheduleByDate(DateTime startDate, DateTime endDate)
        {
            return await mediator.Send(new GetAllDoctorScheduleQuery(startDate, endDate));
        }

    }
}
