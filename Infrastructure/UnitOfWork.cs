using BikeStore.Models;
using BikeStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BikeStoresContext _context;

        public IOrderRepository Orders { get; }

        public UnitOfWork(BikeStoresContext bookStoreDbContext,
            IOrderRepository orders)
        {
            this._context = bookStoreDbContext;
            this.Orders = orders;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
