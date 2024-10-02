
using System.ComponentModel.DataAnnotations;

namespace CustomerSolutionAPI.Models;

public class Customer
{
    public Guid Id { get; set; }
    
    [StringLength(64)]
    [Required]
    public string FirstName { get; set; }
    
    [StringLength(64)]
    [Required]
    public string LastName { get; set; }
    
    [StringLength(64)]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }
}