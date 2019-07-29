using System;
using System.Globalization;
using System.Windows.Data;
using DBManager.Default.Tree;

namespace DBManager.Application.Convertors
{
	public class DbEntityEnumConverter : IValueConverter
	{
		private DbEntityType targetValue;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			targetValue = (DbEntityType)value;
			return targetValue.HasFlag((DbEntityType)parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return targetValue ^= (DbEntityType)parameter;
		}

	}
}