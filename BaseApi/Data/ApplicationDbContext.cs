using Microsoft.EntityFrameworkCore;

namespace BaseApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.createDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.updateDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
