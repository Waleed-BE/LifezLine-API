using System;

namespace CleanCQRS.Core.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
