using System;
using System.Globalization;
using System.Windows;

namespace DBManager.Application.Convertors
{
    public class BooleanToVisibilityConverter : ConverterBase
    {
        public bool Hide { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool newValue && newValue ? Visibility.Visible : Hide ? Visibility.Hidden : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}
