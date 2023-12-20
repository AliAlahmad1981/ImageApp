using ImageApp.Entites;
using Microsoft.EntityFrameworkCore;

namespace ImageApp.Context;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Image>(img =>
        {
            img.HasKey(k => k.Id);
        });
    }

    public DbSet<Image> Images { get; set; }
}