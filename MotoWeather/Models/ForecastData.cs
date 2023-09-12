using Newtonsoft.Json;

namespace MotoWeather.Models
{
    public class ForecastData
    {
        [JsonProperty("cod")]
        public string Cod { get; set; }

        [JsonProperty("message")]
        public double Message { get; set; }

        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public List<ForecastItem> ForecastItems { get; set; }

        [JsonProperty("city")]
        public ForecastCity City { get; set; }
    }

    public class ForecastItem
    {
        [JsonProperty("dt")]
        public long Timestamp { get; set; }

        [JsonProperty("main")]
        public ForecastMainWeatherData MainData { get; set; }

        [JsonProperty("weather")]
        public List<ForecastWeather> WeatherConditions { get; set; }

        [JsonProperty("clouds")]
        public ForecastCloudsData Clouds { get; set; }

        [JsonProperty("wind")]
        public ForecastWindData Wind { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("pop")]
        public double ProbabilityOfPrecipitation { get; set; }

        [JsonProperty("rain")]
        public ForecastRainData Rain { get; set; }

        [JsonProperty("sys")]
        public ForecastSysData Sys { get; set; }

        [JsonProperty("dt_txt")]
        public string DateTimeText { get; set; }
    }

    public class ForecastMainWeatherData
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

        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public int GroundLevel { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("temp_kf")]
        public double TemperatureKf { get; set; }
    }

    public class ForecastWeather
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

    public class ForecastCloudsData
    {
        [JsonProperty("all")]
        public int Cloudiness { get; set; }
    }

    public class ForecastWindData
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Direction { get; set; }

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }

    public class ForecastRainData
    {
        [JsonProperty("3h")]
        public double RainVolumeLast3Hours { get; set; }
    }

    public class ForecastSysData
    {
        [JsonProperty("pod")]
        public string PartOfDay { get; set; }
    }

    public class ForecastCity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public ForecastCoord Coordinates { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }

    public class ForecastCoord
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }

}
