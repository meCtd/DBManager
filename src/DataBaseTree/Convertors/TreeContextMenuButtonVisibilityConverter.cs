using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default.Tree;

namespace DBManager.Application.Convertors
{
    class TreeContextMenuButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = (MetadataViewModelBase)value;
            var cds = (MetadataType)parameter;

            if (vm is null)
                return Visibility.Collapsed;

            return (cds.HasFlag(vm.Type)) && !(vm is CategoryViewModel)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
