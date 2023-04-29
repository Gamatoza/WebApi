using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Repository.PostgreSQL;
using WebApi.Services;
using WebApi.Services.Interfaces;
using WebApi.Services.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemsService _orderItemService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IOrderItemsService orderItemService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> Get(Guid id)
        {
            _logger.LogInformation($"Fetch order by id {id}");

            var order = await _orderService.GetById(id);
            if (order == null || order.IsDeleted)
            {
                return BadRequest($"Order with id: {id} doesn't exist");
            }

            var orderModel = new OrderModel
            {
                Id = order.Id,
                Created = order.Created.ToGeneralTime(),
                Status = (Status)order.Status,
            };

            var orderItems = await _orderItemService.GetByOrderId(order.Id);
            if (orderItems == null)
            {
                orderModel.Lines = new List<OrderItemsModel>();
            }
            else
            {
                orderModel.Lines = orderItems.Select(x => new OrderItemsModel
                {
                    Id = x.Id,
                    Quantity = x.Quantity
                });
            }

            return Ok(orderModel);
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Create(OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest($"Order doesn't exists");
            }

            if (!orderModel.Lines.Any())
            {
                return BadRequest($"Order should have some lines");
            }

            if (orderModel.Lines.All(x => x.Quantity <= 0))
            {
                return BadRequest($"Order quantity can't be less then one");
            }

            var order = new OrderEntity
            {
                Id = orderModel.Id
            };
            await _orderService.Insert(order);
            var orderItems = orderModel.Lines.Select(x => new OrderItemsEntity
            {
                OrderId = order.Id,
                Quantity= x.Quantity
            });

            await _orderItemService.InsertCollection(orderItems);
            
            orderModel.Status = (Status)order.Status;
            orderModel.Created = order.Created.ToGeneralTime(); //TODO: to extension

            return Ok(orderModel);
        }

        [HttpPut]
        public async Task<ActionResult<OrderModel>> Update(OrderModel orderModel)
        {
            var order = await _orderService.GetById(orderModel.Id);

            if(order == null)
            {
                return BadRequest($"Order with id: {orderModel.Id} doesn't exist");
            }

            if (order.Status != (int)Status.New && order.Status != (int)Status.Wait)
            {
                return BadRequest("Updatable order status can't be Paid, Forward, Delivered, Completed");
            }

            order.Status = (int)orderModel.Status;

            var orderItems = await _orderItemService.GetByIds(orderModel.Lines.Select(x => x.Id));
            //var updatedOrderItems = orderModel.Lines.Select(x => x.Id).Intersect(orderItems.Select(o => o.Id)); //TODO: update method
            
            foreach (var item in orderItems)
            {
                var findOrder = orderModel.Lines.FirstOrDefault(x => x.Id == item.Id);
                if(findOrder != null)
                {
                    item.Quantity = findOrder.Quantity;
                }
            }

            await _orderService.Update(order);
            await _orderItemService.UpdateCollection(orderItems);
            return Ok(orderModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderModel>> Delete(Guid id)
        {
            var order = await _orderService.GetById(id);

            if (order == null || order.IsDeleted)
            {
                return BadRequest($"Order with id: {id} doesn't exist");
            }

            if (order.Status != (int)Status.New && order.Status != (int)Status.Wait && order.Status != (int)Status.Paid)
            {
                return BadRequest("Deletable order status can't be Paid, Forward, Delivered, Completed");
            }

            await _orderService.Delete(id);


            return Ok(order);
        }

    }
}
