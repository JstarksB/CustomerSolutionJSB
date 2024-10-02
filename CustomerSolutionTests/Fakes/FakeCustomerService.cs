using CustomerSolution.Models;
using CustomerSolution.Services;

namespace CustomerSolutionTests.Fakes;

public class FakeCustomerService() : ICustomerService
{
    public readonly List<Customer> Customers = [];

    public FakeCustomerService(Customer customer) : this()
    {
        Customers = [customer];
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return Customers;
    }

    public async Task<Customer?> GetCustomerAsync(Guid id)
    {
        return Customers.Find(x => x.Id.Equals(id));
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        Customers.Add(customer);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        Customers.RemoveAll(x => x.Id.Equals(customer.Id));
        Customers.Add(customer);
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        Customers.RemoveAll(x => x.Id.Equals(customerId));
    }
}