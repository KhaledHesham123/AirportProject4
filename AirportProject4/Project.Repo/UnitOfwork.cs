using AirportProject4.Project.core;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo.Data.Context;
using AirportProject4.Project.Repo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace AirportProject4.Project.Repo
{
    public class UnitOfwork : IunitofWork
    {
        private readonly AirlineDbContext _dbContext;

       public IDbContextTransaction? _Transaction { get; set; }
        public IFlightRepo FlightRepo { get; private set; }

        public ISetsRepo SetsRepo { get; private set; }

        public ITicketsrepo TicketsRepo { get; private set; }


        public UnitOfwork(AirlineDbContext dbContext)
        {
            this._dbContext = dbContext;

            FlightRepo = new FlyightRepo(_dbContext);
            SetsRepo = new SeatsRepo(_dbContext);
            TicketsRepo = new TicketRepo(_dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }


        public async Task BeginTransactionAsync()
        {
            _Transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_Transaction != null)
            {
                await _Transaction.CommitAsync();
                await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }
        public async Task RollbackTransactionAsync()
        {
            if (_Transaction != null)
            {
                await _Transaction.RollbackAsync();
                await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }
        public void Dispose()
        {
            _Transaction?.Dispose();
            _dbContext.Dispose();
        }

      
    }
}
