using Microsoft.EntityFrameworkCore;
using ShophupRazor.ModelsR;

namespace ShophupRazor.DataR
{
    public class AppDbContextR:DbContext
    {
        public AppDbContextR(DbContextOptions<AppDbContextR> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryR>().HasData(
                new CategoryR { Id = 1, Name = "Action", DisplayOrder = 1 },
                new CategoryR { Id = 2, Name = "SaiFi", DisplayOrder = 2 },
                new CategoryR { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
        public DbSet<CategoryR> CategoriesR { get; set; }
    }
}
