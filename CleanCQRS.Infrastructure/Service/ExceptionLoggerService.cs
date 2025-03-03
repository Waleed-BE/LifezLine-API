using CleanCQRS.Core.Entities;
using CleanCQRS.Infrastructure.ApplicationDbContext;

namespace CleanCQRS.Infrastructure.Service
{
    public class ExceptionLoggerService
    {
        private readonly AppDbContext _context;

        public ExceptionLoggerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            var exceptionLog = new ExceptionLog
            {
                ExceptionMessage = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                CreatedAt = DateTime.UtcNow
            };

            await _context.TblExceptionLogs.AddAsync(exceptionLog);
            await _context.SaveChangesAsync();
        }
    }
}
