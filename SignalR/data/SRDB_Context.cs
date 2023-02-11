using Microsoft.EntityFrameworkCore;
using SignalR.Models;
using System.Reflection;

namespace SignalR.data
{
    public class SRDB_Context:DbContext
    {
        public SRDB_Context(DbContextOptions<SRDB_Context> option) : base(option)
        {

        }
        public virtual DbSet<call> calls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           // FixedData.SeedData(modelBuilder);
        }

    }
}
