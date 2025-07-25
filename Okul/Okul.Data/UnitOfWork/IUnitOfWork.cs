﻿using System;
namespace Okul.Data.UnitOfWork
{
	public interface IUnitOfWork
	{
        Task<int> SaveChangesAsync(); // Saves changes to the database asynchronously, returns the number of affected rows

        Task BeginTransaction();  // Begins a new transaction asynchronously

        Task CommitTransaction();   // Commits the current transaction asynchronously

        Task RollBackTransaction();
    }
}

