using MotoWeather.Models;
using static MotoWeather.Converters.Converters;

namespace MotoWeather.Views;

public partial class ForecastPage : ContentPage
{
    ForecastData Data { get; set; }

    public ForecastPage(ForecastData forecastData)
    {
        InitializeComponent();

        if (Microsoft.Maui.Devices.DeviceInfo.Platform != Microsoft.Maui.Devices.DevicePlatform.WinUI)
        {
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
        }
        else
        {
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        }

        Resources.Add("WindSpeedConverter", new WindSpeedConverter());
        Resources.Add("DateTimeFormatConverter", new DateTimeFormatConverter());
        Resources.Add("TemperatureRound", new TemperatureRound());

        Data = forecastData;

        ListViewForecastFull.ItemsSource = Data.ForecastItems.ToList();

        //Loading(false);
    }

    private void Loading(bool a)
    {
        loadingIndicator.IsVisible = a;
        loadingIndicator.IsRunning = a;
    }

    private void returnButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}