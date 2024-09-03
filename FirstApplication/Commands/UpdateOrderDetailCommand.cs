using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class UpdateOrderDetailCommand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
