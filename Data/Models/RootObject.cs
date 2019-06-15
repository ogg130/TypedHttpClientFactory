using Newtonsoft.Json;

namespace TypedHttpClientFactory.Data.Models
{
    //******* To automatically build the models that are required for deserialization, simply copy the JSON that 
    //would come over from your API in a browser or a tool like postman and then paste it in to http://json2csharp.com 
    //to have a class extracted for you. (also under Edit->Paste Special->Paste JSON as classes if you are using 
    //Visual Studio) *******

    public class RootObject
    {
        //Annotate with JsonProperty if a key value in the API's response contain spaces or strange characters
        [JsonProperty("_base")]
        public string basecurrency { get; set; }
        
        //The api contains a JSON array keyed as "Rates". If the response returned a JSON array keyed as 
        //"Rates - Today:" you could annotate as follows:
        //[JsonObject("Rates - Today:")]
        public Rates rates { get; set; }

        public string date { get; set; }
    }
}
