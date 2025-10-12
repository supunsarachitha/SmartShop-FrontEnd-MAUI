using System.Globalization; 

namespace SmartShop.MAUI.Converters
{
    public class ServerStatusColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string serverStatus)
            {
                return serverStatus switch
                {
                    "Online" => Colors.Green,  // Green for online
                    "Offline" => Colors.Red,   // Red for offline
                    _ => Colors.Gray           // Gray for unknown or checking
                };
            }

            return Colors.Gray; // Default color
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
