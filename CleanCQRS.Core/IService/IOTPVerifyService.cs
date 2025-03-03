using CleanCQRS.Core.Dtos.OTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCQRS.Core.ServiceInterface
{
    public interface IOTPVerifyService
    {
        public Task<VerifyOtpResponseDto> verifyOTP(VerifyOtpDto verifyOTP);
    }
}
