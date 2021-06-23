using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}