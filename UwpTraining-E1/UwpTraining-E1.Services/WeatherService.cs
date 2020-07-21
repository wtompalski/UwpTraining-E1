using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UwpTraining_E1.Services.Contracts;
using UwpTraining_E1.Services.Model;

namespace UwpTraining_E1.Services
{
    public class WeatherService : IWeatherService
    {
        private string weatherServiceUrl = "http://api.weatherapi.com/v1/current.json?key=e5bde0bca9784e54a0755717201305&q=";

        public async Task<WeatherInfoModel> GetWeatherAsync(string location)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(new Uri(weatherServiceUrl + location));

                var result = JsonConvert.DeserializeObject<WeatherInfoModel>(response);

                result.Current.Condition.Icon = "http:" + result.Current.Condition.Icon;

                await Task.Delay(1500);

                return result;
            }
        }
    }
}
