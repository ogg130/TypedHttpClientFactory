using System.Net.Http;
using System.Threading;

namespace TypedHttpClientFactory.Services
{
    public interface ITypedHttpClientService
    {
        HttpClient Client { get; }
    }
}