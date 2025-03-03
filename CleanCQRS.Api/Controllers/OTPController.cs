using CleanCQRS.Application.Command;
using CleanCQRS.Core.Dtos.OTP;
using CleanCQRS.Core.Options;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CleanCQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController(IMediator mediator, IOptions<ConnectionStringsOptions> options) : ControllerBase
    {
        [HttpPost("Verify-Otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDto verifyOtpDto)
        {
            var result = await mediator.Send(new VerifyOTPCommand(verifyOtpDto));
            return Ok(result);
        }
    }
}
