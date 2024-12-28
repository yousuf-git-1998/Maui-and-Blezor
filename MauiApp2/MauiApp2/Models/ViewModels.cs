using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Models
{
    public class ProductCategory
    {

        public int ProductCategoryID { get; set; }

        public string Name { get; set; }

        public List<Product>? Products { get; set; } = new List<Product>();
    }

    public class Product
    {

        public int ProductID { get; set; }


        public string Name { get; set; }


        public string ProductNumber { get; set; }

        public string Color { get; set; }


        public decimal StandardCost { get; set; }


        public decimal ListPrice { get; set; }


        public int Size { get; set; }


        public decimal Weight { get; set; }


        public int ProductCategoryID { get; set; }
    }
}
