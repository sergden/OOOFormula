using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public Products Products { get; set; }

        public string ImageName { get; set; }
    }
}
