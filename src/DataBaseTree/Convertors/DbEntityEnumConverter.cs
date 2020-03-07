using System;
using System.Globalization;
using System.Windows.Data;
using DBManager.Default.Tree;

namespace DBManager.Application.Convertors
{
	public class DbEntityEnumConverter : ConverterBase
	{
		private MetadataType _targetValue;

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			_targetValue = (MetadataType)value;
			return _targetValue.HasFlag((MetadataType)parameter);
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return _targetValue ^= (MetadataType)parameter;
		}

	}
}