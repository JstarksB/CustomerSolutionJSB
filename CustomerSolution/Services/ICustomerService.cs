using CustomerSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSolution.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerAsync(Guid id);
    Task AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Guid customerId);

}