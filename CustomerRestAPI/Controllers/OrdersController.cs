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

        // GET: api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_orderService.GetAllOrders());
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
