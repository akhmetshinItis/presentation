using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

namespace Presentation;

[Route("documentsDapper/")]
public class DocumentsDapperController(string connectionString) : ControllerBase
{
    private IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString);
    }

    // GET: documents
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        using (var connection = CreateConnection())
        {
            var query = "SELECT * FROM \"Documents\"";
            var documents = await connection.QueryAsync<Document>(query);
            return Ok(documents);
        }
    }

    // GET: documents/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        using (var connection = CreateConnection())
        {
            var query = "SELECT * FROM \"Documents\" WHERE \"Id\" = @Id";
            var document = await connection.QueryFirstOrDefaultAsync<Document>(query, new { Id = id });

            if (document == null)
                return NotFound();

            return Ok(document);
        }
    }

    // POST: /documents
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Document document)
    {
        using (var connection = CreateConnection())
        {
            var query = "INSERT INTO \"Documents\" (\"Id\", \"Content\", \"Title\") VALUES (@Id, @Content, @Title)";
            await connection.ExecuteAsync(query, document);
            return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);
        }
    }

    // PUT: documents/{id}
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Document document)
    {
        using (var connection = CreateConnection())
        {
            var query = "UPDATE \"Documents\" SET \"Content\" = @Content, \"Title\" = @Title WHERE \"Id\" = @Id";
        
            var rowsAffected = await connection.ExecuteAsync(query, new { document.Content, document.Title, document.Id });
        
            if (rowsAffected == 0)
            {
                return NotFound();
            }
        
            return Ok();
        }
    }

    // DELETE: documents/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        using (var connection = CreateConnection())
        {
            var query = "DELETE FROM \"Documents\" WHERE \"Id\" = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }
    }
}