using System.Net.Http;

namespace TypedHttpClientFactory.Services
{
    public interface ITypedHttpClientService
    {
        HttpClient Client { get; }
    }
}