using System;
using System.Globalization;
using System.Windows;

namespace DBManager.Application.Convertors
{
    public class InvertedBooleanConverter : ConverterBase<InvertedBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue ? !boolValue : DependencyProperty.UnsetValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
