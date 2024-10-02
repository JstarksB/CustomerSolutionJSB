using Microsoft.EntityFrameworkCore;

namespace CustomerSolutionAPI.Models;

public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = null!;
}