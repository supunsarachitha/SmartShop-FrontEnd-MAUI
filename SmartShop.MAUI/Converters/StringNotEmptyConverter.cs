using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SmartShop.MAUI.Converters
{
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return !string.IsNullOrWhiteSpace(str);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("StringNotEmptyConverter does not support ConvertBack.");
        }
    }
}