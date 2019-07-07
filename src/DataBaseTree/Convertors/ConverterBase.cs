using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DataBaseTree.Convertors
{
    public abstract class ConverterBase<TConverterType> : MarkupExtension, IValueConverter where TConverterType : class, new()
    {
        private static TConverterType _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new TConverterType());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
