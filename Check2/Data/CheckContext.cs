using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCore.Model;

namespace Check2.Models
{
    public class CheckContext : DbContext
    {
        public CheckContext (DbContextOptions<CheckContext> options)
            : base(options)
        {
        }

        public DbSet<TestCore.Model.Pets> Pets { get; set; }
    }
}
