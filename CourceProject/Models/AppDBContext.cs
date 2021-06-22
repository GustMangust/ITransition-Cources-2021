﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourceProject.Models {
  public class AppDbContext : IdentityDbContext {
    private readonly DbContextOptions _options;
    public AppDbContext(DbContextOptions options) : base(options) {
      _options = options;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
    }
    public DbSet<Fanfic> Fanfics { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
  }
}
