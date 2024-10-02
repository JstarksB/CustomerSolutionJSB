using CustomerSolution.Models;
using CustomerSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSolution.Controllers;

public class CustomerController(ICustomerService customerService) : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(CustomerAddViewModel customerAddViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(customerAddViewModel);

            var customer = customerAddViewModel.ToEntity();
        
            await customerService.AddCustomerAsync(customer); 
        
            return RedirectToAction("Details", new { id = customer.Id });
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }

    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            var response = await customerService.GetCustomerAsync(id);
            return View(response?.ToViewModel());
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }
    }   
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, CustomerViewModel customerViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(customerViewModel);
            }

            if (id != customerViewModel.Id)
            {
                ModelState.AddModelError("Id", "Something went wrong. Please try again later.");
                return View(customerViewModel);
            }

            await customerService.UpdateCustomerAsync(customerViewModel.ToEntity());
            
            return RedirectToAction("Details", new { id = customerViewModel.Id });
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }
    } 
    
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var response = await customerService.GetCustomerAsync(id);
            return View(response?.ToViewModel());
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        try
        {
            await customerService.DeleteCustomerAsync(id);
            return RedirectToAction("Grid");
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }

    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            var customer = await customerService.GetCustomerAsync(id);
            return View(customer?.ToViewModel());
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Grid()
    {
        try
        {
            var customers = await customerService.GetAllCustomersAsync();
            return View(customers.Select(x => x.ToViewModel()));
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(e, true));
        }
        
    }
}