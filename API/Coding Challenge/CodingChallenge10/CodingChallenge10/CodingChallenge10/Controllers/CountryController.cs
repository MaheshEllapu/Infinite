using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingChallenge10.Models;

namespace CodingChallenge10.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countries = new List<Country>
        {
            new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { ID = 2, CountryName = "USA", Capital = "Washington D.C." },
            new Country { ID = 3, CountryName = "Japan", Capital = "Tokyo" }
        };

        // GET
        [HttpGet]
        public IHttpActionResult GetAllCountries()
        {
            return Ok(countries);
        }

        // GET_ID
        [HttpGet]
        public IHttpActionResult GetCountry(int id)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            return Ok(country);
        }

        // POST
        [HttpPost]
        public IHttpActionResult AddCountry(Country country)
        {
            if (country == null)
                return BadRequest("Invalid country data");

            countries.Add(country);
            return Created($"api/country/{country.ID}", country);
        }

        // PUT
        [HttpPut]
        public IHttpActionResult UpdateCountry(int id, Country updatedCountry)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            country.CountryName = updatedCountry.CountryName;
            country.Capital = updatedCountry.Capital;

            return Ok(country);
        }

        // DELETE
        [HttpDelete]
        public IHttpActionResult DeleteCountry(int id)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
                return NotFound();

            countries.Remove(country);
            return Ok("Deleted successfully");
        }
    }
}
