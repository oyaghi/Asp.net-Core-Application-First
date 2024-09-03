using FirstApplication.Data;
using FirstApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Commands
{
    public class CreateCustomerCommand
    {
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
