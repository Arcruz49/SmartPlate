using Microsoft.EntityFrameworkCore;
using SmartPlate.Domain.Entities;

namespace SmartPlate.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Users> users { get; set; }
    }
}