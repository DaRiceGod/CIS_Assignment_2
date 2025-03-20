using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicWebApp.Models
{
    public class ProductInformation
    {
        //public ProductInformation() { }
        //public ProductInformation(int id, string name, int quantitiy)
        //{
        //    ProductId = id;
        //    ProductName = name;
        //    ProductQuantity = quantitiy;
        //}
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }


    }
}