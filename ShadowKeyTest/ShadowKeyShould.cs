using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ShadowKeyTest;

public class MyEntity
{
    public int Key1;
}

public class MyDbContext : DbContext
{
    public DbSet<MyEntity>? MyEntity { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyEntity>().Property<string?>("ShadowKey");
        modelBuilder.Entity<MyEntity>().HasKey("Key1", "ShadowKey");
    }
}

public class ShadowKeyShould : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions _options;

    public ShadowKeyShould()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _options = new DbContextOptionsBuilder()
            .UseSqlite(_connection)
            .Options;
        _connection.Open();
    }
    
    [Fact]
    public void Test1()
    {
    }
    
    public void Dispose()
    {
        _connection.Close();
    }
}