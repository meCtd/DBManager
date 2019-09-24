using System;
using System.Globalization;
using System.Windows.Data;
using DBManager.Default.Tree;

namespace DBManager.Application.Convertors
{
	public class DbEntityEnumConverter : IValueConverter
	{
		private MetadataType targetValue;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			targetValue = (MetadataType)value;
			return targetValue.HasFlag((MetadataType)parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return targetValue ^= (MetadataType)parameter;
		}

	}
}