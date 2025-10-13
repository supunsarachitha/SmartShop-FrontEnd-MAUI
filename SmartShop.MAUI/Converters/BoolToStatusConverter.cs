using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SmartShop.MAUI.Converters
{
    public class BoolToStatusConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                return isActive ? "Active" : "Inactive";
            }

            return "Unknown";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack is not implemented for BoolToStatusConverter.");
        }
    }
}
