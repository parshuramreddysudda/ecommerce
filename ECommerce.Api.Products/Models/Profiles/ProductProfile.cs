using ECommerce.Api.Products.DB;

namespace ECommerce.Api.Products.Models.Profiles;

public class ProductProfile:AutoMapper.Profile
{
    public ProductProfile()
    {
        CreateMap<DB.Product, ProductModel>().ReverseMap();
    }
    
}