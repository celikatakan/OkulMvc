using System;
using Microsoft.EntityFrameworkCore.Storage;
using Okul.Data.Models;

namespace Okul.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly OkulDbContext _db;

        private IDbContextTransaction _transaction;

        // Constructor injecting the DbContext dependency
        public UnitOfWork(OkulDbContext db)
        {
            _db = db;
        }
        // Begins a new transaction asynchronously
        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }
        // Commits the current transaction asynchronously
        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }
        // Disposes the DbContext resources
        public void Dispose()
        {
            _db.Dispose();
        }
        // Rolls back the current transaction asynchronously
        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }
        // Saves changes to the database asynchronously
        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}

