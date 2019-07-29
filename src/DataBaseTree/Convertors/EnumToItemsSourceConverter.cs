using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace DBManager.Application.Convertors
{
    public class EnumToItemsSourceConverter : ConverterBase<EnumToItemsSourceConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type type && type.IsEnum)
                return type.GetMembers().SelectMany(s => s.GetCustomAttributes(typeof(DescriptionAttribute), false)).Cast<DescriptionAttribute>().Select(s => s.Description);

            return DependencyProperty.UnsetValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
