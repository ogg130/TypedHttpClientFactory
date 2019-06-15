using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TypedHttpClientFactory.Data.Models;

namespace TypedHttpClientFactory.Services
{
    public class LocalExchangeRateService : ControllerBase, ILocalExchangeRatesService
    {

        //Create a private readonly instance of ITypedHttpClientService
        private readonly ITypedHttpClientService _typedHttpClientService;

        //Inject typed httpclient service using dependency injection
        public LocalExchangeRateService(ITypedHttpClientService typedHttpClientService)
        {
            _typedHttpClientService = typedHttpClientService;
        }

        public async Task<IActionResult> GetRateAsync(string baseCurrencyId)
        {
            //1) Setup the URI - definitely don't do this this way (exposing it in your code)
            //This is only done this way for examples sake, use environment variables
            var uri = $"latest?base={baseCurrencyId}";
            var todaysDateAsString = System.DateTime.Now.ToString();

            //2) Use the typed httpclientfactory to asynchronously send a GET request to the API
            HttpResponseMessage response = await _typedHttpClientService.Client.GetAsync(uri);

            //3) If the response is not a 200 ok
            if (!response.IsSuccessStatusCode)
            {
                //Return basic error message
                return NotFound($"Failure: {response.StatusCode} / {response.Content}");
            }

            //4) Pull the contents of the response into a string
            var responseString = response.Content.ReadAsStringAsync().Result;
            
            //5) Deserialize the JSON object into a RootObject object
            var responseObject = JsonConvert.DeserializeObject<RootObject>(responseString);

            //6) Change some data to eventually return an updated response
            responseObject.basecurrency = baseCurrencyId;
            responseObject.rates.AUD = responseObject.rates.BGN * responseObject.rates.USD + 5;
            responseObject.date = todaysDateAsString;

            //7) Serialize the updated JSON object into a string
            var processedString = JsonConvert.SerializeObject(responseObject);


            //8) Return the modified copy of the response from the original api
            return Ok(processedString);
        }
    }
}

