using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MotoWeather
{
    public static class MauiProgram
    {
        //User Secrets
        private const string Namespace = "MotoWeather";
        private const string FileName = "secrets.json";

        // Agrega estas dos variables estáticas para almacenar las claves
        public static string WeatherAPIKey { get; private set; }
        public static string Password { get; private set; }
        //


        public static MauiApp CreateMauiApp()
        {
            //User Secrets
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{Namespace}.{FileName}");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            var builder = MauiApp.CreateBuilder();
            builder.Configuration.AddConfiguration(config);
            //

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //User Secrets keys
            WeatherAPIKey = config.GetSection("WeatherAPIKey").Value;
            Password = config.GetSection("Password").Value;
            //
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}