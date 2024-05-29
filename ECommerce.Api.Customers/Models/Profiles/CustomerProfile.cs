using ECommerce.Api.Customers.DB;

namespace ECommerce.Api.Customers.Models.Profiles;

public class CustomerProfile:AutoMapper.Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerModel>().ReverseMap();
    }
}