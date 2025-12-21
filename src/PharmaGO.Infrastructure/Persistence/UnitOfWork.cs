using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PharmaGO.Application.Common.Interfaces;

namespace PharmaGO.Infrastructure.Persistence;

public class UnitOfWork(PharmaGOContext db) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await db.SaveChangesAsync(cancellationToken);
    }
}