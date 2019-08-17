using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Models
{
    public interface IPhoneNumber
    {

        [JsonProperty("valid")]
        bool Valid { get; set; }

        [JsonProperty("number")]
        string Number { get; set; }

        [JsonProperty("local_format")]
        string LocalFormat { get; set; }

        [JsonProperty("international_format")]
        string InternationalFormat { get; set; }

        [JsonProperty("country_prefix")]
        string CountryPrefix { get; set; }

        [JsonProperty("country_code")]
        string CountryCode { get; set; }

        [JsonProperty("country_name")]
        string CountryName { get; set; }

        [JsonProperty("location")]
        string Location { get; set; }

        [JsonProperty("carrier")]
        string Carrier { get; set; }

        [JsonProperty("line_type")]
        string LineType { get; set; }
    }
}
