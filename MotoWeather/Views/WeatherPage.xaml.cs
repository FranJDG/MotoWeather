using MotoWeather.Controllers;
using MotoWeather.Models;

namespace MotoWeather.Views;

public partial class WeatherPage : ContentPage
{

    private WeatherData weatherData { get; set; }
    private ForecastData forecastData { get; set; }

    public WeatherPage()
    {
        InitializeComponent();

    }

    private async void LoadWeatherDataAsync(string city)
    {
        Loading(true);

        WeatherController weatherController = new WeatherController();

        try
        {
            weatherData = await weatherController.GetWeatherAsync(city);
            forecastData = await weatherController.GetForecastAsync(city);
        }
        catch (Exception)
        {
            startMessage.IsVisible = true;
            Main.IsVisible = false;
            Loading(false);
            return;
        }

        DataBinding();

    }

    private void DataBinding()
    {
        this.BindingContext = weatherData;


        temperature.Text = Math.Round(weatherData.MainData.Temperature).ToString() + "º";
        humidity.Text = weatherData.MainData.Humidity.ToString() + "%";
        windSpeed.Text = WindToKmH(weatherData.Wind.Speed).ToString() + " Km/h";
        windDirection.Rotation = weatherData.Wind.Direction;
        string sunrise = UnixToTime(weatherData.Sys.Sunrise, weatherData.Timezone);
        string sunset = UnixToTime(weatherData.Sys.Sunset, weatherData.Timezone);
        sun.Text = sunrise + " - " + sunset;
        description.Text = weatherData.WeatherConditions[0].Description;
        string temperatureMax = Math.Round(weatherData.MainData.TemperatureMax).ToString() + "º";
        string temperatureMin = Math.Round(weatherData.MainData.TemperatureMin).ToString() + "º";
        string feelsLike = Math.Round(weatherData.MainData.FeelsLike).ToString() + "º";
        temperatureInfo.Text = temperatureMax + " / " + temperatureMin + " Sensación térmica " + feelsLike;

        //ListViewForecast.ItemsSource = forecastData.ForecastItems.ToList();

        startMessage.IsVisible = false;
        Main.IsVisible = true;

        Loading(false);
    }

    private void Loading(bool a)
    {
        loadingIndicator.IsVisible = a;
        loadingIndicator.IsRunning = a;
    }

    private void Search_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(cityEntry.Text))
        {
            LoadWeatherDataAsync(cityEntry.Text);
            cityEntry.Text = string.Empty;
        }
        else
        {
            return;
        }
    }

    private string UnixToTime(long unix, int timezone)
    {
        long tiempoUnix = unix + timezone; // Reemplaza esto con tu valor de tiempo Unix

        // Crear una fecha base en el 1 de enero de 1970
        DateTime fechaBase = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Agregar los segundos del tiempo Unix a la fecha base
        DateTime fechaHora = fechaBase.AddSeconds(tiempoUnix);

        // Obtener las horas y minutos
        int horas = fechaHora.Hour;
        int minutos = fechaHora.Minute;

        return fechaHora.ToString("HH:mm");
    }

    private double WindToKmH(double ms)
    {
        double kmh = Math.Round(ms * 60 * 60 / 1000);

        return kmh;
    }

}

/*
 * Como puedes apreciar, a partir de 34ºC la presencia de viento incrementa la sensación térmica en lugar de reducirla. 
 * Esto tiene mucho sentido y es que nuestra temperatura corporal está entre 35ºC y 37ºC por lo que en altas temperaturas, 
 * el viento nos da una sensación de estar metidos en un horno por lo que el calor se intensifica.
 * 
 * Para temperaturas inferiores a nuestra temperatura corporal, la sensación térmica disminuye a medida que hace más viento. 
 * Esto es algo a tener muy en cuenta ya que en temperaturas de sensación térmica muy altas, empezamos a correr riesgos para la salud:
 * 
 * Precaución: entre 27ºC y 32ºC. Podemos sentir fatiga si estamos mucho tiempo al sol o hacemos ejercicio con estas temperaturas.
 * Precaución extrema: entre 32ºC y 40ºC. Con estas temperaturas ya corremos el riesgo de sufrir una insolación, golpes de calor, calambres musculares.
 * Peligro: entre 40º y 55º: se aumenta la probabilidad de sufrir alguno de los problemas visto en el punto anterior.
 * Peligro extremo: temperaturas superiores a los 55º en sensación térmica. En este caso, nuestra salud corre grave peligro y 
 * podemos sufrir un golpe de calor o una insolación en cualquier momento.
 * 
 * En los últimos años es habitual alcanzar 40º en muchas ciudades de España por lo que si sopla un poco el viento, la sensación térmica 
 * puede aumentar un par de grados, situándonos en una situación de peligro.
 */