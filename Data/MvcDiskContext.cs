using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mvc.Models;

namespace Mvc.Data
{
    public class MvcDiskContext : DbContext
    {
        public MvcDiskContext (DbContextOptions<MvcDiskContext> options)
            : base(options)
        {
        }

        public DbSet<Disk> Disk { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
    }
}
