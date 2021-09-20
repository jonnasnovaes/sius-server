using System.Reflection;
using Microsoft.EntityFrameworkCore;
using sius_server.Models;

namespace sius_server.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Vacina> Vacina { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<SolicitarVacina> SolicitarVacina { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}