using Microsoft.EntityFrameworkCore;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class PharmaGOContext : DbContext
{
    public PharmaGOContext(DbContextOptions<PharmaGOContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
}
