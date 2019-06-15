using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TypedHttpClientFactory.Services;

namespace TypedHttpClientFactory.Controllers
{

    [Route("rest/[controller]/")]
    [ApiController]
    public class LocalExchangeRatesController : ControllerBase
    {

        //Create a private readonly property using the service interface to
        //inject into the controller for use using dependency injection
        private readonly ILocalExchangeRatesService _localExchangeRatesService;

        //Use constructor injection to make the service's methods available to your class
        public LocalExchangeRatesController(ILocalExchangeRatesService localExchangeRatesService)
        {
            _localExchangeRatesService = localExchangeRatesService;
        }

        // Make an HTTP GET call to an API to get the latest available exchange rates in the base 
        // currency type of a users choice in JSON format, make changes to the data that was recieved
        // and return the modified data as a JSON data sctructure.
        [HttpGet("rate/{baseCurrencyId}")]
        public async Task<IActionResult> GetRateAsync(string baseCurrencyId)
        {
            //Use the injected service - call the GetRateAsyc method
            return await _localExchangeRatesService.GetRateAsync(baseCurrencyId);
        }
    }
}
