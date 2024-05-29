using AutoMapper;
using ECommerce.Api.Customers.DB;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers;

public class CustomerProvider:ICustomerProvider
{
    private readonly CustomerDbContext _dbContext;
    private readonly ILogger<Customer> _logger;
    private readonly IMapper _mapper;

    public CustomerProvider(CustomerDbContext dbContext,ILogger<Customer> logger,IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _dbContext = dbContext;
        SeedData();
    }

    private void SeedData()
    {
        if (!_dbContext.Customers.Any())
        {
            _dbContext.Customers.Add(new DB.Customer() { Id = 1, FirstName = "John", LastName = "Doe", Gender = "Male", Phone = "1234567890", ProfilePic = "pic1.jpg", UserId = 101 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 2, FirstName = "Jane", LastName = "Smith", Gender = "Female", Phone = "9876543210", ProfilePic = "pic2.jpg", UserId = 102 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 3, FirstName = "Michael", LastName = "Johnson", Gender = "Male", Phone = "5556667777", ProfilePic = "pic3.jpg", UserId = 103 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 4, FirstName = "Emily", LastName = "Brown", Gender = "Female", Phone = "4443332222", ProfilePic = "pic4.jpg", UserId = 104 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 5, FirstName = "David", LastName = "Martinez", Gender = "Male", Phone = "7778889999", ProfilePic = "pic5.jpg", UserId = 105 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 6, FirstName = "Sarah", LastName = "Taylor", Gender = "Female", Phone = "2221113333", ProfilePic = "pic6.jpg", UserId = 106 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 7, FirstName = "Christopher", LastName = "Lee", Gender = "Male", Phone = "6665554444", ProfilePic = "pic7.jpg", UserId = 107 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 8, FirstName = "Jessica", LastName = "Wang", Gender = "Female", Phone = "9990001111", ProfilePic = "pic8.jpg", UserId = 108 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 9, FirstName = "Daniel", LastName = "Garcia", Gender = "Male", Phone = "1112223333", ProfilePic = "pic9.jpg", UserId = 109 });
            _dbContext.Customers.Add(new DB.Customer() { Id = 10, FirstName = "Amanda", LastName = "Lopez", Gender = "Female", Phone = "3334445555", ProfilePic = "pic10.jpg", UserId = 110 });
            _dbContext.SaveChanges();
        }
    }
    public async Task<(bool IsSuccess, IEnumerable<CustomerModel> orders, string ErrorMessage)> GetCustomersAsync()
    {
        try
        {
            var customers = await _dbContext.Customers.ToListAsync();
            if (customers != null && customers.Any())
            {
                var results=_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(customers);
                return (true, results, null);
            }

            return (false, null, "Not Found");
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
        }

    }

    public async Task<(bool IsSuccess, CustomerModel order, string ErrorMessage)> GetCustomerAsync(int id)
    {
        try
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(p=>p.Id==id);
            if (customer != null)
            {
                var result=_mapper.Map<DB.Customer, CustomerModel>(customer);
                return (true, result, null);
            }

            return (false, null, "Not Found");
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
        }
    }
}