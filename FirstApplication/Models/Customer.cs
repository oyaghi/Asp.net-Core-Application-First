using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string? Address { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [Phone]
        public string Phone { get; set; } = null!;


        public virtual ICollection<Order> Orders { get; set; } = null!;
    }
}