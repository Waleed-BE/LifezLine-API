using CleanCQRS.Core.Dtos.OTP;
using CleanCQRS.Core.Entities;
using CleanCQRS.Core.ServiceInterface;
using CleanCQRS.Infrastructure.ApplicationDbContext;
using CleanCQRS.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace CleanCQRS.Infrastructure.Service
{
    public class OTPVerifyService : IOTPVerifyService
    {
        private readonly AppDbContext _appDbContext;
        public OTPVerifyService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<VerifyOtpResponseDto> verifyOTP(VerifyOtpDto verifyOTP)
        {
            var getUserRecordByOTP = await _appDbContext
                                                .TblUser
                                                .Where(x => x.OTP == verifyOTP.OTP && x.IsOtpVerified == false)
                                                .FirstOrDefaultAsync();

            if(getUserRecordByOTP == null)
            {
                return new VerifyOtpResponseDto() { Message = "No User found."};
            }
          
            if (DateTimeExtensions.IsIn1MinuteRangeOf((DateTime)getUserRecordByOTP.OTPGeneratedUTCTime, verifyOTP.UTCDatetime))
            {
                getUserRecordByOTP.IsOtpVerified = true;
                _appDbContext.TblUser.Update(getUserRecordByOTP);
                await _appDbContext.SaveChangesAsync();
                return new VerifyOtpResponseDto() { Message = "OTP verified successfully." };
            }
            else
            {
                return new VerifyOtpResponseDto() { Message = "OTP expired" };
            }
        }
    }
}
