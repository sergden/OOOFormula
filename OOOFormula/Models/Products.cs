﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Models
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string ImagesName { get; set; }

        public bool status { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public int MaterialsId { get; set; }
        public Materials Materials { get; set; }


        public int ManufacturersId { get; set; }
        public Manufacturers Manufacturers { get; set; }

    }
}
