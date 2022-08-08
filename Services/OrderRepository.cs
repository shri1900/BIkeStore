using BikeStore.Infrastructure;
using BikeStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Services
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BikeStoresContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersByOrderStatus(int status)
        {
            return await _context.Orders.Where(c => c.OrderStatus == status).ToListAsync();
        }
    }
}
