using System;
using System.Windows;
using System.Globalization;
namespace DataBaseTree.Convertors
{
    public class InvertedBooleanToVisibilityConverter : ConverterBase<InvertedBooleanToVisibilityConverter>
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
