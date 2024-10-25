using API.Payment.Global.Stripe.Domain.Implementation.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace API.Payment.Global.Stripe.Domain.Implementation.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(
                ILogger<ConsumerService> logger
            )
        {
            _logger = logger;
        }

        private HttpResponseMessage GetHttpClient(string ApiKeyStripe, string UrlBase, string Endpoint, HttpMethod? httpMethod = null)
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(UrlBase + Endpoint);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApiKeyStripe}:")));

            var Request = new HttpRequestMessage();

            if (httpMethod is not null && httpMethod!.Method == HttpMethod.Post.Method)
            {
                return httpClient.PostAsync(UrlBase + Endpoint, null).Result;
            }
            else
            {
                return httpClient.SendAsync(Request).Result;
            }
        }

        public async Task<T?> GetHttpClientSingle<T>(string ApiKeyStripe, string UrlBase, string Endpoint, HttpMethod? httpMethod = null)
        {
            var Data = GetHttpClient(ApiKeyStripe, UrlBase, Endpoint, httpMethod);

            if (Data.IsSuccessStatusCode)
            {
                var DataString = Data.Content.ReadAsStringAsync().Result;
                try
                {
                    var DataStringJson = (dynamic)JObject.Parse(DataString);
                    var DataStringJsonText = JsonConvert.SerializeObject(DataStringJson);

                    return JsonConvert.DeserializeObject<T>(DataStringJsonText);
                }
                catch (Exception Ex)
                {
                    _logger.LogError(Ex.Message);
                }
            }

            return default;
        }

        public async Task<List<T>?> GetHttpClientMultiple<T>(string ApiKeyStripe, string UrlBase, string Endpoint, string NameCollectionReturn, HttpMethod? httpMethod = null)
        {
            var lst = new List<T>();

            var Data = GetHttpClient(ApiKeyStripe, UrlBase, Endpoint, httpMethod);

            if (Data.IsSuccessStatusCode)
            {
                var DataString = Data.Content.ReadAsStringAsync().Result;
                try
                {
                    var DataSearchNameCollectionReturn = (dynamic)JObject.Parse(DataString)[NameCollectionReturn]!;
                    var DataSearchNameCollectionReturnJsonText = JsonConvert.SerializeObject(DataSearchNameCollectionReturn);

                    foreach (var item in JsonConvert.DeserializeObject<List<T>>(DataSearchNameCollectionReturnJsonText))
                    {
                        lst.Add(item);
                    }
                }
                catch (Exception Ex)
                {
                    _logger.LogError(Ex.Message);
                }
            }

            return lst;
        }
    }
}