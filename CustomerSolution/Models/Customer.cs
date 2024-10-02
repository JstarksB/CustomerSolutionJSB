using System.ComponentModel.DataAnnotations;

namespace CustomerSolution.Models;

public class Customer
{
    [Key]
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

    public CustomerViewModel ToViewModel()
    {
        return new CustomerViewModel
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            EmailAddress = EmailAddress
        };
    }
}