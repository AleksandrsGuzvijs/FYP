using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LibFYP.DTOs;

namespace WebApi.Facades
{
    /// <summary>
    /// Class PostOrderFacade.
    /// </summary>
    public class PostOrderFacade
    {
        private readonly HttpClient _client;
        private readonly string _url = "post orders microservice link"; // add link later
        private JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Default constructor that sets up the HttpClient.
        /// </summary>
        public PostOrderFacade()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        /// <summary>
        /// Constructor used for testing that accepts a mock HttpCient.
        /// </summary>
        /// <param name="client"></param>
        public PostOrderFacade(HttpClient client)
        {
            this._client = client;
            _serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
        }

        /// <summary>
        /// post order as an asynchronous operation.
        /// </summary>
        /// <param name="orders">The selection box orders.</param>
        /// <returns></returns>
        public async Task<Order> PostOrder(Order order)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_url),
                    Content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json")
                };

                return await ExecuteRequestAsync<Order>(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Async task to execute a HttpRequestMessage and return a single model. Uses T type parameter so the facade can be expanded if needed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T> ExecuteRequestAsync<T>(HttpRequestMessage request) where T : class
        {
            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content, _serializerSettings);
        }
    }
}