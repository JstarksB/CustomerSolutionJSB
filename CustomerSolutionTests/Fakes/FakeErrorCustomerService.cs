using CustomerSolution.Models;
using CustomerSolution.Services;

namespace CustomerSolutionTests.Fakes;

public class FakeErrorCustomerService : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        throw new HttpRequestException();
    }

    public async Task<Customer?> GetCustomerAsync(Guid id)
    {
        throw new HttpRequestException();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        throw new HttpRequestException();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        throw new HttpRequestException();
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        throw new HttpRequestException();
    }
}