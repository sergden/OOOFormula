using Newtonsoft.Json;
using System.Collections.Generic;

namespace OOOFormula.Models
{
    public class RecaptchaResponse
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("ErrorCodes")]
        public List<string> ErrorCodes { get; set; }
    }
}
