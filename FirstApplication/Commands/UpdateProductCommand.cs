using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Commands
{
    public class UpdateProductCommand
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
    }
}
