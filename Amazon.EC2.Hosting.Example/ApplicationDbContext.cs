using Amazon.EC2.Hosting.Example.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Amazon.EC2.Hosting.Example
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
