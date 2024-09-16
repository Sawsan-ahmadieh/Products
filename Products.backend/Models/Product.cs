using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.backend.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName="nvarchar(150)")]
        public string Name { get; set; }
        [Column(TypeName ="nvarchar(500)")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } 
        public int Stock { get; set; }
        public double Rating { get; set; }
    }
}
