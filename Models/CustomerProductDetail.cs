﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicWebApp.Models
{
    public class CustomerProductDetail
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}