namespace API.Payment.Global.Stripe.Domain.Implementation.Interfaces
{
    public interface IConsumerService
    {
        Task<T?> GetHttpClientSingle<T>(string ApiKeyStripe, string UrlBase, string Endpoint, HttpMethod? httpMethod = null);

        Task<List<T>?> GetHttpClientMultiple<T>(string ApiKeyStripe, string UrlBase, string Endpoint, string NameCollectionReturn, HttpMethod? httpMethod = null);
    }
}