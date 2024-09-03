using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class LoginCustomerCommand
    {

        [Key]
        public int Id { get; set; }


        [Required]
        public string FirstName { get; set; } = null!;



        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [Phone]
        public string Phone { get; set; } = null!;

    }
}
