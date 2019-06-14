using System;
using System.Net.Http;

namespace TypedHttpClientFactory.Services
{
    public class TypedHttpClientService : ITypedHttpClientService
    {
        public HttpClient Client { get; }
        
        public TypedHttpClientService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.openrates.io/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }
    }
}
