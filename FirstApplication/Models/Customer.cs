using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Address { get; set; } = null!;
        
        public string Phone { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = null!;
    }
}