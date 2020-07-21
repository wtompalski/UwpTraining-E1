using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpTraining_E1.Services.Model;

namespace UwpTraining_E1.Services.Contracts
{
    public interface IWeatherService
    {
        Task<WeatherInfoModel> GetWeatherAsync(string location);
    }
}
