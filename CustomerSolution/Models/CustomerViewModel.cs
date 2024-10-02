using System.ComponentModel.DataAnnotations;

namespace CustomerSolution.Models;

public class CustomerViewModel
{
    [Key]
    public Guid Id { get; set; }
    
    [StringLength(64)]
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    
    [StringLength(64)]
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    
    [StringLength(64)]
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address")]
    public string EmailAddress { get; set; }

    public Customer ToEntity()
    {
        return new Customer
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            EmailAddress = EmailAddress
        };
    }
}