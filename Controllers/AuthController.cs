using BasicWebApp.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicWebApp.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

    [HttpPost]
        public ActionResult Login(string email, string password)
        {
            string PrintString;

            //if (email == "1@gmail.com" && password == "1")
            //{

            //    PrintString = "Authorized";
            //}
            //else
            //{
            //    PrintString = "Unauthorized";
            //}

            UserDataAccess userDataAccess = new UserDataAccess();

            if (userDataAccess.GetUser(email, password) || (email == "Name" && password == "1234"))
            {

                PrintString = "Authorized";
            }
            else
            {
                PrintString = "Unauthorized";
            }


            ViewBag.Message = PrintString;

            return View();
        }

        //public ActionResult UserGet(string username, string password)
        //{
        //    UserDataAccess userDataAccess = new UserDataAccess();
        //    userDataAccess.GetUser(username, password);

        //    return View("Login");
        //}

        public ActionResult ForgotPassword(string user)
        {
            string PrintString;
            UserDataAccess userDataAccess = new UserDataAccess();
            if (userDataAccess.ResetPassword(user))
            {
                PrintString = "Password changed";
                ViewBag.Message = PrintString;
            }
            return View();
        }
    }
}