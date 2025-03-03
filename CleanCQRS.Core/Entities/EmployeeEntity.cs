using System.ComponentModel.DataAnnotations;

namespace CleanCQRS.Core.Entities
{
    public class EmployeeEntity
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public string Designation { get; set; } = null!;
    }
}
