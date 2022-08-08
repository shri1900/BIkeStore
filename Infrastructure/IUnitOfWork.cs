using BikeStore.Models;
using BikeStore.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace BikeStore.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }

        int Save();
    }
}
