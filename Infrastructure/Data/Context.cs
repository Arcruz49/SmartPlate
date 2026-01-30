using Microsoft.EntityFrameworkCore;
using SmartPlate.Domain.Entities;

namespace SmartPlate.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
    }
}