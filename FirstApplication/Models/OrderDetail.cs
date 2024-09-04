using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int? ProductId{ get; set; } // Making it nullable
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;



    }
}
