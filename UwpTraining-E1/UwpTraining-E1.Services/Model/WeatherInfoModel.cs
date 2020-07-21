using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpTraining_E1.Services.Model
{
    public class WeatherInfoModel
    {
        [JsonProperty("location")]
        public LocationModel Location { get; set; }

        [JsonProperty("current")]
        public CurrentWeatherModel Current { get; set; }
    }

    public class LocationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class CurrentWeatherModel
    {
        [JsonProperty("temp_c")]
        public double TempC { get; set; }

        [JsonProperty("condition")]
        public ConditionModel Condition { get; set; }
    }

    public class ConditionModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
