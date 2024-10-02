using CustomerSolution.Models;

namespace CustomerSolution.Services;

public class CustomerService(HttpClient httpClient, ILogger<CustomerService> logger) : ICustomerService
{
    private const string ApiAddress = "/api/Customer";

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<Customer>>(ApiAddress);
            return response ?? [];
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Unable to retrieve customers");
            throw;
        }

    }

    public async Task<Customer?> GetCustomerAsync(Guid id)
    {
        try
        {
            var address = ApiAddress + "/" + id;
            var response = await httpClient.GetFromJsonAsync<Customer>(address);
            return response;
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Unable to retrieve customer {id}", id);
            throw;
        }
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(ApiAddress, customer);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Unable to add new customer");
            throw;
        }
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(ApiAddress + $"/{customer.Id}", customer);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Unable to update customer {id}", customer.Id);
            throw;
        }
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        try
        {
            var response = await httpClient.DeleteAsync(ApiAddress + $"/{customerId}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Unable to delete customer {id}", customerId);
            throw;
        }
    }
}