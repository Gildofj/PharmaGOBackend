using Microsoft.EntityFrameworkCore;
using PharmaGO.Core.Entities;

namespace PharmaGO.Infrastructure.Persistence;

public class PharmaGOContext(DbContextOptions<PharmaGOContext> options) : DbContext(options)
{
    public DbSet<Client> Client { get; set; } = null!;
    public DbSet<Employee> Employee { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Pharmacy> Pharmacy { get; set; } = null!;
}