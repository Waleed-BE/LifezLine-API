using CleanCQRS.Core.Dtos.OTP;
using CleanCQRS.Core.ServiceInterface;
using MediatR;

namespace CleanCQRS.Application.Command
{
    public record VerifyOTPCommand(VerifyOtpDto verifyOtpDTO) : IRequest<VerifyOtpResponseDto>;
    public class VerifyOTPCommandHandler(IOTPVerifyService OTPService, IMediator mediator) : IRequestHandler<VerifyOTPCommand, VerifyOtpResponseDto>
    {
        public async Task<VerifyOtpResponseDto> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            return await OTPService.verifyOTP(request.verifyOtpDTO);

        }
    }
}
