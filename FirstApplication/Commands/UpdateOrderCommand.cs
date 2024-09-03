using FirstApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class UpdateOrderCommand
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderPlaced { get; set; }
        [Required]
        public DateTime? OrderFulfilled { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }






    }

}
}
