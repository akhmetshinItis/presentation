using System.Data;
using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Presentation;

[MemoryDiagnoser]
public class BenchmarkTests
{
    private readonly IDbConnection _db;
    private readonly DocumentsContext _context;

    public BenchmarkTests(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);

        var options = new DbContextOptionsBuilder<DocumentsContext>()
            .UseNpgsql(connectionString)
            .Options;

        _context = new DocumentsContext(options);
    }


    [Benchmark]
    public async Task EF_Insert()
    {
        var document = new Document { Content = "EF Test", Title = "text"};
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
    }

    [Benchmark]
    public async Task Dapper_Insert()
    {
        var document = new Document { Id = Guid.NewGuid(), Content = "Dapper Test", Title = "text"};
        var query = "INSERT INTO \"Documents\" (\"Id\", \"Content\", \"Title\") VALUES (@Id, @Content, @Title)";
        await _db.ExecuteAsync(query, document);
    }
}
