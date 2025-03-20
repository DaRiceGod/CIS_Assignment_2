using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BasicWebApp.Models
{
    public class USER
    {
        public USER() { }
        public USER(string name, string pass) 
        {
            userName = name;
            password = pass;
        }

        public String userName { get; set; }
        public String password { get; set; }

       

    }
}