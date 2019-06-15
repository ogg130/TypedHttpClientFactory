using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TypedHttpClientFactory.Services
{
    public interface ILocalExchangeRatesService
    {
        Task<IActionResult> GetRateAsync(string baseCurrencyId);
    }
}