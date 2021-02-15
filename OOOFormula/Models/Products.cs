using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Models
{
    public class Products
    {
        public int Id{ get; set; }

        public string Name{ get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> ImagesName { get; set; }

        public int ManufacturerId { get; set; }

        public int MaterialId { get; set; }

    }
}
