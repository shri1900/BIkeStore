using BikeStore.Infrastructure;
using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Services
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public interface IOrderRepository : IGenericRepository<Order>
        {
            Task<IEnumerable<Order>> GetOrdersByOrderName(string orderName);
        }
    }
}
