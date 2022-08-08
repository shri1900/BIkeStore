using BikeStore.Infrastructure;
using BikeStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeStoreController : ControllerBase
    {
        private readonly ILogger<BikeStoreController> logger;
        private readonly IUnitOfWork _unitOfWork;

        public BikeStoreController(ILogger<BikeStoreController> logger,
                                        BikeStoresContext bikeStoresContext,
                                        IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                logger.Log(LogLevel.Information, "Inside BikeStoreController --> Get");
                var orderList = _unitOfWork.Orders.GetAll();
                if (orderList == null) return NotFound();
                
                logger.Log(LogLevel.Information, "Finished BikeStoreController --> Get");
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            try
            {
                logger.Log(LogLevel.Information, "Inside BikeStoreController --> Get for id");
                var order = _unitOfWork.Orders.GetById(id);

                if (order == null)
                {
                    return BadRequest("Order not found");
                }
                
                logger.Log(LogLevel.Information, "Finished BikeStoreController --> Get for id");
                return Ok(order);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostAsync(Order order)
        {
            try
            {
                logger.Log(LogLevel.Information, "Inside BikeStoreController --> Post");
                _unitOfWork.Orders.Insert(order);
                int rowCount = _unitOfWork.Save();
                
                logger.Log(LogLevel.Information, "Finished BikeStoreController --> Post");
                return Ok(rowCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            try
            {
                logger.Log(LogLevel.Information, "Inside BikeStoreController --> Put");
                var newOrder = _unitOfWork.Orders.GetById(order.OrderId);
                
                if (newOrder != null)
                {
                    newOrder.OrderDate = order.OrderDate;
                    newOrder.OrderStatus = order.OrderStatus;
                    newOrder.ShippedDate = order.ShippedDate;
                    // we can update whatever is required
                }

                int rowCount = _unitOfWork.Save();
                logger.Log(LogLevel.Information, "Finished BikeStoreController --> Put");
               
                return Ok(rowCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        public async Task<ActionResult<int>> Delete(Order order)
        {
            try
            {
                logger.Log(LogLevel.Information, "Inside BikeStoreController --> Delete");

                var tobeDeletedOrder = _unitOfWork.Orders.GetById(order.OrderId);

                if (tobeDeletedOrder == null)
                {
                    return BadRequest("Order not found");
                }

                _unitOfWork.Orders.Delete(tobeDeletedOrder);
                int rowCount = _unitOfWork.Save();

                logger.Log(LogLevel.Information, "Finished BikeStoreController --> Delete");
                return Ok(rowCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.ToString());
            }
        }
    }
}
