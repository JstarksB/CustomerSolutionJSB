using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerSolutionAPI.Models;

namespace CustomerSolutionAPI.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController(CustomerContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, [FromBody]Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            
            try
            {
                context.Customers.Update(customer);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                    
                throw;
            }
        
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomerAsync([FromBody]Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid id)
        {
            return context.Customers.Any(x => x.Id == id);
        }
    }
}
