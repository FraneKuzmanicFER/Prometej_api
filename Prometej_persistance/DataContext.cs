using Microsoft.EntityFrameworkCore;
using Prometej_core.Models.efModels;

namespace Prometej_persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
    }
}
