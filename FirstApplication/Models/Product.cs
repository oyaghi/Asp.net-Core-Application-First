using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApplication.Models
{
    public class Product
    {
        [Key] //Indication for primary key 
        public int Id { get; set; } // Using Capitalized for Naming convention
        
        public string Name { get; set; } = null!; // (!)null-forgiving operator, by that you mean trust me i know it can take a null 
       
        public decimal Price { get; set; }
    }
}
