
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.EntityFrameworkCore;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// builder.Services.AddDbContext<ProductsDbContext>(optionsAction =>
// {
//     optionsAction.UseInMemoryDatabase("Products");
// });
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrdersService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient("OrdersService", configureClient =>
{
    configureClient.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
});
builder.Services.AddHttpClient("ProductsService", configureClient =>
{
    configureClient.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("CustomerService", configureClient =>
{
    configureClient.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
}).AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
 {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(options =>
{
    options.MapControllers();
});
app.Run();