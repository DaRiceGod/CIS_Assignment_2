using BasicWebApp.DataAccessLayer;
using BasicWebApp.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicWebApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        [HttpGet]
        public ActionResult UserInfo()
        {
            var model = new List<USER>(); // Ensure this is properly populated
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertPrd(string name, string password)
        {
            UserDataAccess userDataAccess = new UserDataAccess();
            string message;

            if (userDataAccess.InsertProduct(name, password))
            {
                message = ("Name: " + name + "\nPassword: " + password);
            }
            else
            {
                message = "Error";
            }
            ViewBag.message = message;
            return View();
        }

        public ActionResult InsertPrd()
        {
            return View();
        }

        //public ActionResult Display()
        //{
        //    ProductDataAccess data = new ProductDataAccess();
        //    List<ProductCustomer> list = data.ListProduct();

        //    return View(list);
        //}

        public ActionResult Display()
        {
            ProductDataAccess data = new ProductDataAccess();
            List<CustomerProductDetail> list = data.ListProductCustomerInfo();

            return View(list);

        }

        public ActionResult ProductInformation()
        {
            ProductDataAccess data = new ProductDataAccess();
            List<ProductInformation> list = data.listProducts();

            return View(list);
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(string productName, int productId = 0, int productQuantity = 0)
        {
            productId = 0;
            ProductDataAccess data = new ProductDataAccess();
            string message;
            if (data.InsertProduct(productName, productId, productQuantity))
            {
                TempData["CreateMessage"] = $"Product Name: " + productName + "\nProduct Quantity: " + productQuantity;
            }
            else
            {
                TempData["CreateMessage"] = "Error";
            }
            //ViewBag.message = message;
            return RedirectToAction("ProductInformation");

        }

        [HttpGet]
        public ActionResult DeleteProduct(int productId)
        {
            ProductDataAccess data = new ProductDataAccess();
            ProductInformation prd = data.GetProductByID(productId);

            return View(prd);
        }

        //Redirects for form POST.
        [HttpPost]
        [ActionName("DeleteProduct")] //Applies to the DeleteProduct name to keep the URL the same
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int productId)
        {

            ProductDataAccess data = new ProductDataAccess();
            bool isDeleted = data.DeleteProduct(productId);

            if (isDeleted)
            {
                TempData["DeleteMessage"] = $"Product ID {productId} deleted successfully.";
            }
            else
            {
                TempData["DeleteMessage"] = "Error deleting product.";
            }

            return RedirectToAction("ProductInformation");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId)
        {
            ProductDataAccess data = new ProductDataAccess();
            ProductInformation prd = data.GetProductByID(productId);
            return View(prd);
        }

        [HttpPost]
        //[ActionName("EditProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(string productName, int productId, int productQuantity)
        {
            ProductDataAccess data = new ProductDataAccess();
            bool isUpdated = data.EditProduct(productName, productId, productQuantity);
            if (isUpdated)
            {
                TempData["EditMessage"] = $"Product ID {productId} updated successfully.";
            }
            else
            {
                TempData["EditMessage"] = "Error updating product.";
            }

            return RedirectToAction("ProductInformation");


        }
    }
}