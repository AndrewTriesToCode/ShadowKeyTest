using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ShadowKeyTest;

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

        // Create and seed test database
        var context = new MyDbContext(_options);
        context.ShadowKeyValueOnGenerate = "value1";
        context.Database.EnsureCreated();

        var entity1 = new MyEntity() { Key1 = 1 };
        context.MyEntity!.Add(entity1);
        context.SaveChanges();
    }
    
    [Fact]
    public void RemoveFreshlyAttachedEntry()
    {
        var context = new MyDbContext(_options);
        context.ShadowKeyValueOnGenerate = "value1";
        
        var entity1 = new MyEntity { Key1 = 1 };
        
        // Error here that the shadow key property isn't known although the generator has provided the value
        context.Remove(entity1);
        
        context.SaveChanges();
        
        Assert.Empty(context.MyEntity!.ToList());
    }
    
    public void Dispose()
    {
        _connection.Close();
    }
}