using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UwpTraining_E1.Commands;
using UwpTraining_E1.Services;
using UwpTraining_E1.Services.Contracts;
using UwpTraining_E1.Services.Model;
using UwpTraining_E1.ViewModels.Base;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace UwpTraining_E1.ViewModels
{
    public class WeatherViewModel : BindableBase
    {
        private RelayCommand refreshCommand;
        private IWeatherService service = new WeatherService();

        public WeatherViewModel()
        {
            this.refreshCommand = new RelayCommand(
                async () => { await this.Refresh(); },
                () => this.CanRefresh);
        }

        private WeatherInfoModel weather;

        public WeatherInfoModel WeatherInfo
        {
            get { return weather; }
            set
            {
                this.SetProperty(ref this.weather, value);
                this.OnPropertyChanged(nameof(IsReady));
            }
        }

        public ICommand RefreshCommand => this.refreshCommand;

        private string city;

        public string City
        {
            get { return city; }
            set
            {
                this.SetProperty(ref this.city, value);
                this.refreshCommand.RaiseCanExecuteChanged();
            }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.SetProperty(ref this.isBusy, value);
                this.OnPropertyChanged(nameof(IsReady));
            }
        }

        public bool IsReady => !this.IsBusy && this.WeatherInfo != null;

        private bool CanRefresh => !string.IsNullOrEmpty(this.city);

        private async Task Refresh()
        {
            this.IsBusy = true;

            try
            {
                // OPTION 1
                // var dispatcher = Window.Current.Dispatcher;

                // OPTION 2
                var synchContext = SynchronizationContext.Current;

                WeatherInfoModel localWeather = await this.service.GetWeatherAsync(this.city).ConfigureAwait(false);

                // OPTION 1
                // await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                // {
                //    this.WeatherInfo = localWeather;
                //    this.IsBusy = false;
                // });

                // OPTION 2
                synchContext.Post(obj => 
                {
                    this.WeatherInfo = obj as WeatherInfoModel;
                    this.IsBusy = false;
                }, localWeather);
            }
            finally
            {
                // this.IsBusy = false;
            }
        }
    }
}
