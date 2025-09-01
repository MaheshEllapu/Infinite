using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingChallenge10.Models;
 
namespace CodeChallenge10.Controllers
{
    public class OrdersController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        // 1. Get all orders of employee with EmployeeID = 5 (Buchanan Steven)
        [HttpGet]
        [Route("api/orders/byemployee/{employeeId}")]
        public IHttpActionResult GetOrdersByEmployee(int employeeId)
        {
            var orders = db.Orders
                           .Where(o => o.EmployeeID == employeeId)
                           .Select(o => new
                           {
                               o.OrderID,
                               o.OrderDate,
                               o.ShipCountry,
                               o.Customer.ContactName
                           })
                           .ToList();

            if (!orders.Any())
                return NotFound();

            return Ok(orders);
        }

        // 2. Call stored procedure: GetCustomersByCountry
        [HttpGet]
        [Route("api/customers/bycountry/{country}")]
        public IHttpActionResult GetCustomersByCountry(string country)
        {
            // EF imports stored procs as functions
            var customers = db.GetCustomersByCountry(country).ToList();

            if (!customers.Any())
                return NotFound();

            return Ok(customers);
        }
    }
}
