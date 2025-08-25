using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Coding_Challenge9
{
    public class Movie
    {
        public int Mid { get; set; }
        public string MovieName { get; set; }
        public string DirectorName { get; set; }
        public DateTime DateOfRelease { get; set; }
    }

    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}

