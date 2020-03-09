using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Markup;
using Framework.Attributes;

namespace DBManager.Application.Convertors
{
    public class EnumConverter : MarkupExtension, IValueConverter
    {
        private static readonly IDictionary<Type, IEnumerable<ValueDescription>> _cache = new Dictionary<Type, IEnumerable<ValueDescription>>();

        private readonly Type _enumType;

        public EnumConverter(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException(nameof(enumType));

            _enumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_cache.TryGetValue(_enumType, out var values))
                return values;

            values = _enumType
                .GetMembers(BindingFlags.Static | BindingFlags.Public)
                .Where(s => !Attribute.IsDefined(s, typeof(HiddenAttribute)))
                .Select(s => new ValueDescription((Enum)Enum.Parse(_enumType, s.Name), s.GetCustomAttribute<DescriptionAttribute>()?.Description ?? s.Name));

            _cache[_enumType] = values;

            return values;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class ValueDescription
    {
        public Enum Value { get; }

        public string Description { get; }

        public ValueDescription(Enum value, string description)
        {
            Value = value;
            Description = description;
        }

        public static implicit operator Enum(ValueDescription value)
        {
            return value.Value;
        }
    }
}
