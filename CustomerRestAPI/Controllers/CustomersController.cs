using ConsoleApp.Core.Entity;
using CustomerApp.Core.ApplicationService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace CustomerRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers -- READ ALL
        
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerService.GetAllCustomers();
        }

        // GET api/customers/5 -- READ by ID
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID must be grater than 0");
            }
            return _customerService.FindCustomerByIDIncludeOrders(id);
        }

        // POST api/customers -- CREATE JSON
        [Authorize (Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                BadRequest("First name is required for Creating Customer");
            }
            if (string.IsNullOrEmpty(customer.LastName))
            {
                BadRequest("Last name is required for Creating Customer");
            }
            return Ok(_customerService.CreateCustomer(customer));
        }

        // PUT api/customers/5 -- UPDATE
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
        {
            if (id < 1 || id != customer.ID)
            {
                return BadRequest("Parameter ID and CustomerID must be the same");
            }
            return Ok(_customerService.UpdateCustomer(customer));
        }

        // DELETE api/customers/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = _customerService.DeleteCustomer(id);

            if (customer == null)
            {
                return StatusCode(404, $"Did not find Customer with ID {id}");
            }
            return Ok($"Customer with ID {id} has been deleted");
        }
    }
}
