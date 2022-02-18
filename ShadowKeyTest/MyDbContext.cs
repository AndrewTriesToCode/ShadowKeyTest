// Copyright Finbuckle LLC, Andrew White, and Contributors.
// Refer to the solution LICENSE file for more inforation.

using Microsoft.EntityFrameworkCore;

namespace ShadowKeyTest;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MyEntity> MyEntity { get; set; }
    public string ShadowKeyValueOnGenerate { get; set; } = "";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyEntity>().Property<string?>("ShadowKey").HasValueGenerator<ShadowKeyGenerator>();
        modelBuilder.Entity<MyEntity>().HasKey("Key1", "ShadowKey");
    }
}