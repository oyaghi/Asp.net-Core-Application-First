using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class UpdateCustomerCommand
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string? Address { get; set; } = null!;
        [MinLength(10)]
        [MaxLength(10)]
        [Phone]
        public string Phone { get; set; } = null!;


    }
}
