using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled  { get; set; }
        public int CustomerId { get; set; }  // FK  It detects the FK based on Naming Coventions 
        public Customer Customer { get; set; } //. Navigation Property that holds the related Customer Object
        public ICollection<OrderDetail> OrderDetail { get; set; }





    }
}
