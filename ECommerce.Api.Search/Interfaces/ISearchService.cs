namespace ECommerce.Api.Search.Interfaces;

public interface ISearchService
{
    Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId);
}