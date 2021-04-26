using System;

namespace ApiProductos.Tables
{
    public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public double price { get; set; }
        public double salePrice { get
            {
                return price * 1.18;
            } 
        }
    }
}
