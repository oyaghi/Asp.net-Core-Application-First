using FirstApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class CreateOrderDetailCommand
    {
        [Required]
        public int Quantity { get; set; }

      
    }
}
