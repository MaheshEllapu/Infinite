using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coding_Challenge9.Models;

namespace Coding_Challenge9.Controllers
{
    public class CodeController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        // 1. Return all customers from Germany
        public ActionResult GermanyCustomers()
        {
            var customers = db.Customers
                              .Where(c => c.Country == "Germany")
                              .ToList();
            return View(customers);
        }

        // 2. Return customer details with OrderId = 10248
        public ActionResult CustomerOrder10248()
        {
            var orderDetails = db.Orders
                .Where(o => o.OrderID == 10248)
                .Select(o => new OrderViewModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    CompanyName = o.Customer.CompanyName,
                    ContactName = o.Customer.ContactName,
                    Country = o.Customer.Country
                })
                .FirstOrDefault();

            return View(orderDetails);
        }
    }
}



