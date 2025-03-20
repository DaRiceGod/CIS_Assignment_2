using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicWebApp.Models
{
    public class ProductCustomer
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public ProductInformation Product { get; set; }
        public CustomerData Customer { get; set; }


        public ProductCustomer() { }

        public ProductCustomer(int customerId, int productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}