using Microsoft.EntityFrameworkCore;

namespace Presentation;

public class DocumentsContext : DbContext
{
    public DbSet<Document> Documents { get; set; }

    public DocumentsContext(DbContextOptions<DocumentsContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
}