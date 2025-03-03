

using System.ComponentModel.DataAnnotations;

namespace CleanCQRS.Core.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
