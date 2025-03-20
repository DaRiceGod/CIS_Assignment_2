using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicWebApp.Models
{
    public class CustomerData
    {
        public CustomerData() { }
        public CustomerData(int id, string name, string email)
        {
            CustomerId = id;
            CustomerName = name;
            CustomerEmail = email;
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}