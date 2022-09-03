using Microsoft.EntityFrameworkCore;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class PharmaGOContext : DbContext
{
    public PharmaGOContext(DbContextOptions<PharmaGOContext> options) : base(options)
    {
    }

    public DbSet<Client> Client { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Pharmacy> Pharmacy { get; set; } = null!;
}
