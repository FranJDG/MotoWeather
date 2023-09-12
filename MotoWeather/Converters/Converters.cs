using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoWeather.Converters
{
    internal class Converters
    {
        // Convertidor de velocidad del viento
        public class WindSpeedConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is double windSpeedInMetersPerSecond)
                {
                    // Realiza la conversión de m/s a km/h (1 m/s = 3.6 km/h)
                    double windSpeedInKilometersPerHour = windSpeedInMetersPerSecond * 3.6;
                    return $"{Math.Round(windSpeedInKilometersPerHour)} km/h";
                }
                return value;
            }

            // Se utiliza solo si se necesita convertir valores desde la interfaz de usuario de vuelta al formato adecuado para el modelo de datos.
            // Este método no se encuentra implementado, se podría eliminar.
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        // Convertidor de formato de fecha y hora
        public class DateTimeFormatConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {

                if (value is string dateTimeString)
                {
                    // El dato devuelto es de tipo string, debo convertirlo primero a datetime
                    if (DateTime.TryParse(dateTimeString, out DateTime dateTime))
                    {
                        return dateTime.ToString("ddd HH:mm");
                    }
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        // Convertidor de formato de temperatura
        public class TemperatureRound : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {

                if (value is double temperature)
                {
                    return $"{Math.Round(temperature)}º";
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
