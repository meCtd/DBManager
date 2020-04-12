using System;
using System.Globalization;
using System.Windows;

namespace DBManager.Application.Convertors
{
    public class InvertedBooleanToVisibilityConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool val && !val ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            return !(value is Visibility visibility) || visibility != Visibility.Visible;
        }
    }
}
