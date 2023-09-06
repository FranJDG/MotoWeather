/*
 * Tenemos que asegurarnos de que los nombres de las propiedades en la clase WeatherData coincidan
 * con los nombres de los campos en el JSON o utilices [JsonProperty] para mapear adecuadamente las
 * propiedades a los nombres de los campos en el JSON.
 * 
 * [JsonProperty("name")]
 *  public string City { get; set; }  
 * 
 */

using Newtonsoft.Json;

namespace MotoWeather.Models
{
    public class WeatherData
    {
        [JsonProperty("name")]
        public string City { get; set; }

        [JsonProperty("weather")]
        public List<Weather> WeatherConditions { get; set; }

        [JsonProperty("main")]
        public MainWeatherData MainData { get; set; }

        [JsonProperty("wind")]
        public WindData Wind { get; set; }

        [JsonProperty("clouds")]
        public CloudsData Clouds { get; set; }

        [JsonProperty("sys")]
        public SysData Sys { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class MainWeatherData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public double TemperatureMin { get; set; }

        [JsonProperty("temp_max")]
        public double TemperatureMax { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class WindData
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Direction { get; set; }
    }

    public class CloudsData
    {
        [JsonProperty("all")]
        public int Cloudiness { get; set; }
    }

    public class SysData
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }



}
