using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coding_Challenge9.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
    }
}