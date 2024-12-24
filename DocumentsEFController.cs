using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation;

[Route("documentsEF/")]
public class DocumentsEFController(DocumentsContext documentsContext) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<List<Document>> GetDocuments()
    {
        var documents = await documentsContext.Documents.ToListAsync();
        return documents;
    }
    
    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<Document>> GetDocument(Guid id)
    {
        // "E7EA478E-6DD3-4203-81DB-9601BEA9D1CF"
        var document = await documentsContext.Documents.FindAsync(id);
        if (document is null)
        {
            return NotFound();
        }
        return document;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDocument([FromBody]Document document)
    {
        var existingDocument = await documentsContext.Documents.FindAsync(document.Id);
        if (existingDocument is not null)
        {
            return Conflict();
        }
        await documentsContext.Documents.AddAsync(document);
        await documentsContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(Guid id)
    {
        var document = await documentsContext.Documents.FindAsync(id);
        if (document is null)
        {
            return NotFound();
        }
        documentsContext.Documents.Remove(document);
        await documentsContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateDocument([FromBody]Document document)
    {
        var existingDocument = await documentsContext.Documents.AsNoTracking().FirstOrDefaultAsync(d => d.Id == document.Id);
        if (existingDocument is null)
        {
            return NotFound();
        }
        documentsContext.Documents.Update(document);
        await documentsContext.SaveChangesAsync();
        return Ok();
    }
}