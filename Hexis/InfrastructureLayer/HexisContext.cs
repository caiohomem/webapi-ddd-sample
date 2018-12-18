using Hexis.DomainModelLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public class HexisContext : DbContext
    {
        public HexisContext(DbContextOptions<HexisContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(b => b.Name)
                .IsUnique();

            //Disable Cascade
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
