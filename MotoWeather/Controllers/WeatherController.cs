using MotoWeather.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace MotoWeather.Controllers
{
    internal class WeatherController
    {
        //Url OpenWeatherMap https://api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}

        private string ApiKey = MauiProgram.WeatherAPIKey;

        //private const string ApiKey = "";
        private const string ApiBaseUrlWeather = "https://api.openweathermap.org/data/2.5/weather";
        private const string ApiBaseUrlForecast = "https://api.openweathermap.org/data/2.5/forecast";

        public async Task<WeatherData> GetWeatherAsync(string city)
        {

            using (HttpClient client = new HttpClient())
            {
                var apiUrl = $"{ApiBaseUrlWeather}?q={city}&appid={ApiKey}&units=metric&lang=es";
                var response = await client.GetStringAsync(apiUrl);

                // Deserializa la respuesta JSON en un objeto WeatherData
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(response);
                return weatherData;
            }


        }

        public async Task<ForecastData> GetForecastAsync(string city)
        {

            using (HttpClient client = new HttpClient())
            {
                var apiUrl = $"{ApiBaseUrlForecast}?q={city}&appid={ApiKey}&units=metric&lang=es";
                var response = await client.GetStringAsync(apiUrl);

                // Deserializa la respuesta JSON en un objeto WeatherData
                ForecastData forecastData = JsonConvert.DeserializeObject<ForecastData>(response);
                return forecastData;
            }


        }
    }
}
