using FirstApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class CreateOrderCommand
    {
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled { get; set; }
        public int CustomerId { get; set; }  

    }
}