using Microsoft.Maui.ApplicationModel;
using MotoWeather.Views;

namespace MotoWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            Application.Current.UserAppTheme = AppTheme.Light;

            MainPage = new WeatherPage();
        }
    }
}