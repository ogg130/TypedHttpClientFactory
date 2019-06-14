using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TypedHttpClientFactory.Data.Models;
using TypedHttpClientFactory.Services;

namespace TypedHttpClientFactory.Controllers
{
    
    [Route("rest/[controller]/")]
    [ApiController]
    public class LocalExchangeRatesController : ControllerBase
    {

        private readonly ITypedHttpClientService _typedHttpClientService;

        //Inject typed httpclient service using dependency injection
        public LocalExchangeRatesController(ITypedHttpClientService typedHttpClientService)
        {
            _typedHttpClientService = typedHttpClientService;
        }

        // GET latest available exchange rates in the base currency type of a users choice
        [HttpGet("base/{baseCurrencyId}")]
        public async Task<IActionResult> GetComic(string baseCurrencyId)
        {
            var uri = $"latest?base={baseCurrencyId}";
            HttpResponseMessage response = await _typedHttpClientService.Client.GetAsync(uri);
            
            if (!response.IsSuccessStatusCode)
            {
                return NotFound($"Failure: {response.StatusCode} / {response.Content}");
            }

            var responseString = response.Content.ReadAsStringAsync().Result;

            var responseJson = JObject.Parse(responseString);

            var rootObject = JsonConvert.DeserializeObject<RootObject>(responseJson.ToString());

            //The _base field comes over without a value. Populate the baseCurrencyId that is 
            //passed into the controller method
            rootObject.basecurrency = baseCurrencyId;

            var processedString = JsonConvert.SerializeObject(rootObject);

            var processedJson = JObject.Parse(processedString);

            //Create a textfile containing the json response
            var logPath = System.IO.Path.GetTempFileName();
            var logWriter = System.IO.File.CreateText(logPath);
            logWriter.WriteLine(processedJson);
            logWriter.Dispose();

            return Ok(processedJson);
        }
    }

}
