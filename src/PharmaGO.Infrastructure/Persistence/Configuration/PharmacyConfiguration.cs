using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaGO.Core.Entities;

namespace PharmaGO.Infrastructure.Persistence.Configuration;

public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
{
    public void Configure(EntityTypeBuilder<Pharmacy> builder)
    {
        builder.ToTable("Pharmacies");
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(100).IsRequired();
            address.Property(a => a.Number).HasColumnName("Number").HasMaxLength(20).IsRequired();
            address.Property(a => a.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(100).IsRequired();
            address.Property(a => a.City).HasColumnName("City").HasMaxLength(100).IsRequired();
            address.Property(a => a.State).HasColumnName("State").HasMaxLength(2).IsRequired();
            address.Property(a => a.Country).HasColumnName("Country").HasMaxLength(100).IsRequired();
            address.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(20).IsRequired();
        });
    }
}