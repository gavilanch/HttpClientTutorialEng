using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
