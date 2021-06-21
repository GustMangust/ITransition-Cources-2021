using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourceProject.Models {
  public class AppDBContext : IdentityDbContext {
    private readonly DbContextOptions _options;
    public AppDBContext(DbContextOptions options) : base(options) {
      _options = options;
    }
    public DbSet<Work> Works { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
    }
  }
}
