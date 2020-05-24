using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default.Tree;

namespace DBManager.Application.Convertors
{
    class SelectTop100RowsButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = (MetadataViewModelBase)value;

            if (vm is null)
                return Visibility.Collapsed;

            return (vm.Type == MetadataType.Table || vm.Type == MetadataType.View) && !(vm is CategoryViewModel)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
