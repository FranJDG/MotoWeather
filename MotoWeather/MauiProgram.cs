using Microsoft.Extensions.Logging;

namespace MotoWeather
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Sono-Bold.ttf", "SonoBold");
                    fonts.AddFont("Sono-ExtraBold.ttf", "SonoExtraBold");
                    fonts.AddFont("Sono-ExtraLight.ttf", "SonoExtraLight");
                    fonts.AddFont("Sono-Light.ttf", "SonoLight");
                    fonts.AddFont("Sono-Medium.ttf", "SonoMedium");
                    fonts.AddFont("Sono-Regular.ttf", "SonoRegular");
                    fonts.AddFont("Sono-SemiBold.ttf", "SonoSemiBold");

                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    //Iconos
                    fonts.AddFont("Brands-Regular-400,otf", "FAB"); //Font awesome brands
                    fonts.AddFont("Free-Regular-400.otf", "FAR"); //Font awesome regular
                    fonts.AddFont("Free-Solid-900.otf", "FAS"); //Font awesome solid
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MIR"); //Material icons regular
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}