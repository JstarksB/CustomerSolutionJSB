using CustomerSolution.Controllers;
using CustomerSolution.Models;
using CustomerSolutionTests.Fakes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CustomerSolutionTests.Tests;

public class CustomerControllerTests
{
    private readonly Customer _customerEntity = new()
    {
        Id = Guid.Parse("E800C70F-DA90-4EB1-95F0-36409FCEDDC3"),
        EmailAddress = "crucible123@gmail.com",
        FirstName = "Arthur",
        LastName = "Miller"
    };
    
    private readonly CustomerViewModel _customerViewModel = new()
    {
        Id = Guid.Parse("E800C70F-DA90-4EB1-95F0-36409FCEDDC3"),
        EmailAddress = "crucible123@gmail.com",
        FirstName = "Arthur",
        LastName = "Miller"
    };
    
    private readonly CustomerAddViewModel _customerAddViewModel = new()
    {
        EmailAddress = "crucible123@gmail.com",
        FirstName = "Arthur",
        LastName = "Miller"
    };

    [Fact]
    public async Task AddCustomer_CorrectData_ShouldCompleteSuccessfully_AndRedirect()
    {
        // Arrange
        var service = new FakeCustomerService();
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Add(_customerAddViewModel);
        
        // Assert
        result.Should().BeOfType<RedirectToActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Details");
        service.Customers.Should().ContainSingle();
        service.Customers.Single().FirstName.Should().Be("Arthur");
        service.Customers.Single().LastName.Should().Be("Miller");
        service.Customers.Single().EmailAddress.Should().Be("crucible123@gmail.com");
        service.Customers.Single().Id.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task AddCustomer_WithModelWarning_ShouldReturnToPageWithoutSaving()
    {
        // Arrange
        var service = new FakeCustomerService();
        var controller = new CustomerController(service);
        controller.ModelState.AddModelError("LastName", "Last Name should be populated");
        var customer = _customerAddViewModel;
        customer.LastName = null!;
        
        // Act
        var result = await controller.Add(customer);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<CustomerAddViewModel>();
        service.Customers.Should().BeEmpty();
    }
    
    [Fact]
    public async Task AddCustomer_ReturningException_ShouldReturnErrorPage()
    {
        // Arrange
        var service = new FakeErrorCustomerService();
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Add(_customerAddViewModel);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<ErrorViewModel>();
        result.As<ViewResult>().Model.As<ErrorViewModel>().Exception.Should().BeOfType<HttpRequestException>();
    }
    
    [Fact]
    public async Task EditCustomer_CorrectData_ShouldCompleteSuccessfully_AndRedirect()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);

        var customer = _customerViewModel;
        customer.LastName = "Dent";
        
        // Act
        var result = await controller.Edit(customer.Id, customer);
        
        // Assert
        result.Should().BeOfType<RedirectToActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Details");
        service.Customers.Should().ContainSingle();
        service.Customers.Single().Should().BeEquivalentTo(customer.ToEntity());
    }

    [Fact]
    public async Task EditCustomer_WithModelWarning_ShouldReturnToPageWithoutSaving()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);
        controller.ModelState.AddModelError("LastName", "Last Name should be populated");

        var customer = _customerViewModel;
        customer.LastName = null!;
        
        // Act
        var result = await controller.Edit(customer.Id, customer);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<CustomerViewModel>();
        service.Customers.Should().ContainSingle();
        service.Customers.Single().Should().BeEquivalentTo(_customerEntity);
    }
    
    [Fact]
    public async Task EditCustomer_WithMismatchingId_ShouldReturnToPageWithoutSaving()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Edit(Guid.Parse("8B520101-07C0-4FB8-9189-B788521A6654"), _customerViewModel);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<CustomerViewModel>();
        service.Customers.Should().ContainSingle();
        service.Customers.Single().Should().BeEquivalentTo(_customerEntity);
    }
    
    [Fact]
    public async Task EditCustomer_ReturningException_ShouldReturnErrorPage()
    {
        // Arrange
        var service = new FakeErrorCustomerService();
        var controller = new CustomerController(service);
        
        var customer = _customerViewModel;
        customer.LastName = "Dent";
        
        // Act
        var result = await controller.Edit(customer.Id, customer);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<ErrorViewModel>();
        result.As<ViewResult>().Model.As<ErrorViewModel>().Exception.Should().BeOfType<HttpRequestException>();
    }
    
    [Fact]
    public async Task DeleteCustomer_CorrectData_ShouldCompleteSuccessfully_AndRedirect()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.DeleteCustomer(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<RedirectToActionResult>();
        result.As<RedirectToActionResult>().ActionName.Should().Be("Grid");
        service.Customers.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteCustomer_ReturningException_ShouldReturnErrorPage()
    {
        // Arrange
        var service = new FakeErrorCustomerService();
        var controller = new CustomerController(service);
        
        var customer = _customerViewModel;
        customer.LastName = "Dent";
        
        // Act
        var result = await controller.Edit(customer.Id, customer);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<ErrorViewModel>();
        result.As<ViewResult>().Model.As<ErrorViewModel>().Exception.Should().BeOfType<HttpRequestException>();
    }
    
    [Fact]
    public async Task Edit_WhenNoErrorsOccur_ShouldCompleteSuccessfully_AndRedirect()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Edit(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<CustomerViewModel>();
        result.As<ViewResult>().Model.As<CustomerViewModel>().Should().BeEquivalentTo(_customerViewModel);
    }
    
    [Fact]
    public async Task Edit_ReturningException_ShouldReturnErrorPage()
    {
        // Arrange
        var service = new FakeErrorCustomerService();
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Edit(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<ErrorViewModel>();
        result.As<ViewResult>().Model.As<ErrorViewModel>().Exception.Should().BeOfType<HttpRequestException>();
    }
    
    [Fact]
    public async Task Delete_WhenNoErrorsOccur_ShouldCompleteSuccessfully_AndRedirect()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Delete(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<CustomerViewModel>();
        result.As<ViewResult>().Model.As<CustomerViewModel>().Should().BeEquivalentTo(_customerViewModel);
    }
    
    [Fact]
    public async Task Delete_ReturningException_ShouldReturnErrorPage()
    {
        // Arrange
        var service = new FakeErrorCustomerService();
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Delete(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<ErrorViewModel>();
        result.As<ViewResult>().Model.As<ErrorViewModel>().Exception.Should().BeOfType<HttpRequestException>();
    }
    
    [Fact]
    public async Task Details_WhenNoErrorsOccur_ShouldCompleteSuccessfully_AndRedirect()
    {
        // Arrange
        var service = new FakeCustomerService(_customerEntity);
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Details(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<CustomerViewModel>();
        result.As<ViewResult>().Model.As<CustomerViewModel>().Should().BeEquivalentTo(_customerViewModel);
    }
    
    [Fact]
    public async Task Details_ReturningException_ShouldReturnErrorPage()
    {
        // Arrange
        var service = new FakeErrorCustomerService();
        var controller = new CustomerController(service);
        
        // Act
        var result = await controller.Details(_customerEntity.Id);
        
        // Assert
        result.Should().BeOfType<ViewResult>();
        result.As<ViewResult>().Model.Should().BeOfType<ErrorViewModel>();
        result.As<ViewResult>().Model.As<ErrorViewModel>().Exception.Should().BeOfType<HttpRequestException>();
    }
}