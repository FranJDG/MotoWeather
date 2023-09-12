using MotoWeather.Controllers;
using MotoWeather.Models;
using static MotoWeather.Converters.Converters;

namespace MotoWeather.Views;

public partial class WeatherPage : ContentPage
{
    private WeatherData weatherData { get; set; }
    private ForecastData forecastData { get; set; }

    public WeatherPage()
    {
        InitializeComponent();

        Resources.Add("WindSpeedConverter", new WindSpeedConverter());
        Resources.Add("DateTimeFormatConverter", new DateTimeFormatConverter());
        Resources.Add("TemperatureRound", new TemperatureRound());
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
        windSpeed.Text = WindToKmH(weatherData.Wind.Speed).ToString() + " km/h";
        windDirection.Rotation = weatherData.Wind.Direction;
        string sunrise = UnixToTime(weatherData.Sys.Sunrise, weatherData.Timezone);
        string sunset = UnixToTime(weatherData.Sys.Sunset, weatherData.Timezone);
        sun.Text = sunrise + " - " + sunset;
        description.Text = weatherData.WeatherConditions[0].Description;
        feelsLike.Text = Math.Round(weatherData.MainData.FeelsLike).ToString() + "º";
        infoUpdate.Text = DateTime.Now.ToString("dddd HH:mm");

        //ListViewForecast.ItemsSource = forecastData.ForecastItems.ToList();

        //Limitar número de horas a mostrar en la prediccicón

        var forecasts = forecastData.ForecastItems.ToList();

        List<ForecastItem> fore = new List<ForecastItem>();

        for (int i = 0; i < 10; i++)
        {
            fore.Add(forecasts[i]);
        }

        ListViewForecast.ItemsSource = fore;

        //Recomendaciones
        RecomendacionesMoto(Math.Round(weatherData.MainData.Temperature), WindToKmH(weatherData.Wind.Speed), weatherData.MainData.Humidity);

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
        double kmh = Math.Round(ms * 3.6);

        return kmh;
    }

    private void updateButton_Clicked(object sender, EventArgs e)
    {
        LoadWeatherDataAsync(city.Text);
    }

    private void RecomendacionesMoto(double temp, double wind, int hum)
    {
        Star0();
        recomendaciones.Text = string.Empty;
        alertas.Text = string.Empty;

        if (temp >= 28 || wind >= 35 || temp < 5)
        {
            recomendaciones.Text = "No es recomendable salir en moto.";

            if ((temp >= 30 || temp < 5) && wind >= 35)
            {
                Star0();
                alertas.Text = "Temperatura y viento extremos.";
                recomendaciones.Text += "\nTen precaución en condiciones de viento fuerte, ya que pueden dificultar el control de la moto. " +
                    "Ajusta tu velocidad y posición en la carretera y mantente alerta.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }

            else if (temp >= 30 || temp < 5)
            {
                Star1();
                alertas.Text = "Temperatura extrema.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
            else if (wind >= 35)
            {
                Star1();
                alertas.Text = "Viento extremo.";
                recomendaciones.Text += "\nTen precaución en condiciones de viento fuerte, ya que pueden dificultar el control de la moto. " +
                    "Ajusta tu velocidad y posición en la carretera y mantente alerta.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
        }
        else if (temp >= 25 && temp < 30)
        {
            recomendaciones.Text = "Recomendable llevar ropa bien ventilada. No olvides el casco y guantes de verano.";

            if (wind >= 25 && wind <= 35)
            {
                Star2();
                alertas.Text = "Precaución, altas temperaturas y fuerte viento.";
                recomendaciones.Text += "\nEn días ventosos, mantén un agarre firme en el manillar y mantén una posición estable. " +
                    "Asegúrate de que tu ropa esté bien ajustada para evitar que se hinche y te haga perder el equilibrio.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
            else if (wind >= 10 && wind < 25)
            {
                Star3();
                alertas.Text = "Precaución, altas temperaturas y posibles ráfagas de viento.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
            else if (wind >= 0 && wind < 10)
            {
                Star4();
                alertas.Text = "Precaución, altas temperaturas.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
        }
        else if (temp >= 20 && temp < 25)
        {
            recomendaciones.Text = "Recomendable llevar ropa bien ventilada. Añade una capa base si hace falta y no olvides el casco y guantes de verano.";

            if (wind >= 25 && wind <= 35)
            {
                Star3();
                alertas.Text = "Precaución, fuerte viento.";
                recomendaciones.Text += "\nEn días ventosos, mantén un agarre firme en el manillar y mantén una posición estable. " +
                    "Asegúrate de que tu ropa esté bien ajustada para evitar que se hinche y te haga perder el equilibrio.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
            else if (wind >= 10 && wind < 25)
            {
                Star4();
                alertas.Text = "Precaución, posibles ráfagas de viento.";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
            else if (wind >= 0 && wind < 10)
            {
                Star5();
                alertas.Text = "Clima ideal!!";
                if (hum < 30)
                {
                    recomendaciones.Text += "\nBebe suficiente agua para evitar la deshidratación, ya que la baja humedad puede hacer que sudes más.";
                }
                else if (hum > 70)
                {
                    recomendaciones.Text += "\nLa alta humedad puede hacer que te sientas más incómodo, especialmente si también hace calor. " +
                        "Usa ropa transpirable y mantente hidratado.";
                }
            }
        }
        else if (temp >= 15 && temp < 20)
        {
            recomendaciones.Text = "Recomendable llevar ropa medianamente cálida. Añade una capa interior si hace falta y utiliza bragas, " +
                "no olvides el casco y guantes de entretiempo.";

            if (wind >= 25 && wind <= 35)
            {
                Star3();
                alertas.Text = "Precaución, fuerte viento.";
                recomendaciones.Text += "\nEn días ventosos, mantén un agarre firme en el manillar y mantén una posición estable. " +
                    "Asegúrate de que tu ropa esté bien ajustada para evitar que se hinche y te haga perder el equilibrio.";
                if (hum > 70)
                {
                    Star2();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías a primeras horas de la mañana.";
                }
            }
            else if (wind >= 10 && wind < 25)
            {
                Star4();
                alertas.Text = "Precaución, posibles ráfagas de viento.";
                if (hum > 70)
                {
                    Star2();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías a primeras horas de la mañana.";
                }
            }
            else if (wind >= 0 && wind < 10)
            {
                Star5();
                alertas.Text = "Clima ideal!!";
                if (hum > 70)
                {
                    Star2();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías a primeras horas de la mañana.";
                }
            }
        }
        else if (temp >= 10 && temp < 15)
        {
            recomendaciones.Text = "Elige una chaqueta de moto cálida y pantalones aislados. Usa capas adicionales como una camiseta térmica " +
                "y unas bragas para el cuello si es necesario. Guantes de invierno.";

            if (wind >= 25 && wind <= 35)
            {
                Star3();
                alertas.Text = "Precaución, fuerte viento.";
                recomendaciones.Text += "\nEn días ventosos, mantén un agarre firme en el manillar y mantén una posición estable. " +
                    "Asegúrate de que tu ropa esté bien ajustada para evitar que se hinche y te haga perder el equilibrio.";
                if (hum > 70)
                {
                    Star2();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías.";
                }
            }
            else if (wind >= 10 && wind < 25)
            {
                Star4();
                alertas.Text = "Precaución, posibles ráfagas de viento.";
                if (hum > 70)
                {
                    Star3();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías.";
                }
            }
            else if (wind >= 0 && wind < 10)
            {
                Star5();
                alertas.Text = "Equípate bien y a disfrutar de la ruta!!";
                if (hum > 70)
                {
                    Star4();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías.";
                }
            }
        }
        else if (temp < 10)
        {
            recomendaciones.Text = "En condiciones de frío extremo, opta por una chaqueta y pantalones de moto con forro térmico. " +
                "Usar ropa térmica debajo es esencial. Lleva guantes cálidos, un pasamontañas y una bufanda o cuello alto para proteger tu cuello y rostro.";

            if (wind >= 25 && wind <= 35)
            {
                Star2();
                alertas.Text = "Bajas temperaturas.";
                alertas.Text += "\nPrecaución, fuerte viento.";
                recomendaciones.Text += "\nEn días ventosos, mantén un agarre firme en el manillar y mantén una posición estable. " +
                    "Asegúrate de que tu ropa esté bien ajustada para evitar que se hinche y te haga perder el equilibrio.";
                if (hum > 70)
                {
                    Star1();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías.";
                }
            }
            else if (wind >= 10 && wind < 25)
            {
                Star3();
                alertas.Text = "Bajas temperaturas.";
                alertas.Text += "\nPrecaución, posibles ráfagas de viento.";
                if (hum > 70)
                {
                    Star2();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías.";
                }
            }
            else if (wind >= 0 && wind < 10)
            {
                Star4();
                alertas.Text = "Bajas temperaturas.";
                if (hum > 70)
                {
                    Star3();
                    alertas.Text += "\nPosibilidad de humedad en curvas sombrías.";
                }
            }
        }
        else
        {
            Star5();
            recomendaciones.Text = "Es adecuado salir en moto."; // Otras condiciones
        }


    }

    private void Star0()
    {
        star1.Color = Colors.White;
        star2.Color = Colors.White;
        star3.Color = Colors.White;
        star4.Color = Colors.White;
        star5.Color = Colors.White;
    }

    private void Star1()
    {
        star1.Color = Colors.Yellow;
        star2.Color = Colors.White;
        star3.Color = Colors.White;
        star4.Color = Colors.White;
        star5.Color = Colors.White;
    }

    private void Star2()
    {
        star1.Color = Colors.Yellow;
        star2.Color = Colors.Yellow;
        star3.Color = Colors.White;
        star4.Color = Colors.White;
        star5.Color = Colors.White;
    }

    private void Star3()
    {
        star1.Color = Colors.Yellow;
        star2.Color = Colors.Yellow;
        star3.Color = Colors.Yellow;
        star4.Color = Colors.White;
        star5.Color = Colors.White;
    }

    private void Star4()
    {
        star1.Color = Colors.Yellow;
        star2.Color = Colors.Yellow;
        star3.Color = Colors.Yellow;
        star4.Color = Colors.Yellow;
        star5.Color = Colors.White;
    }

    private void Star5()
    {
        star1.Color = Colors.Yellow;
        star2.Color = Colors.Yellow;
        star3.Color = Colors.Yellow;
        star4.Color = Colors.Yellow;
        star5.Color = Colors.Yellow;
    }

    private async void pronosticoButton_Clicked(object sender, EventArgs e)
    {
        Loading(true);
        await Task.Delay(100);
        await Navigation.PushModalAsync(new ForecastPage(forecastData));
        Loading(false);
    }
}
