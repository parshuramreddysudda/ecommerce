using ECommerce.Api.Orders.DB;

namespace ECommerce.Api.Orders.Models.Profiles;

public class OrderProfile:AutoMapper.Profile
{
    public OrderProfile()
    {
        CreateMap<DB.Orders, OrderModel>().ReverseMap();
        CreateMap<OrderItem, OrderItemModel>().ReverseMap();
    }
}