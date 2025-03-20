using BasicWebApp.DataAccessLayer;
using BasicWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicWebApp.Controllers
{
    public class USERController : Controller
    {
        // GET: Display
        public ActionResult Display()
        {
            UserDataAccess data = new UserDataAccess();
            List<USER> userlist = data.GetUserList();

            return View(userlist);
        }
    }
}