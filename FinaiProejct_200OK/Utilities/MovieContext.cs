using FinaiProejct_200OK.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Utilities
{
    class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
