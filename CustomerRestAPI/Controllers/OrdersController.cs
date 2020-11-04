using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;


namespace CustomerRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders -- READ ALL
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_orderService.GetFilteredOrders(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            //return Ok(_orderService.GetAllOrders());
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok(_orderService.FindOrderByID(id));
        }

        // POST api/orders
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.ID)
            {
                return BadRequest("Parameter ID and order ID must be the same");
            }
            return Ok(_orderService.UpdateOrder(order));
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
