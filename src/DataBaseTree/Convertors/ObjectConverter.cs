using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DBManager.Application.Convertors
{
    public class ObjectsPair
    {
        public object Input { get; set; }
        public object Output { get; set; }
    }

    public class EnumObjectsPairList : List<ObjectsPair>
    { }

    [ContentProperty(nameof(Items))]
    public class ObjectToObjectConverter : IValueConverter
    {
        public EnumObjectsPairList Items { get; set; } = new EnumObjectsPairList();
        public object FallBackValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
                return FallBackValue;

            var enumDispatcherObjectPair = Items.FirstOrDefault(pair => Equals(pair.Input, value));

            return enumDispatcherObjectPair?.Output ?? FallBackValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
