using System;
using DataBaseTree.Model;
using DataBaseTree.ViewModel.ConnectionViewModel;
using Prism.Mvvm;

namespace DataBaseTree.ViewModel
{
    public class ConnectionWindowViewModel : BindableBase
    {
        #region Fields

        private BaseConnectionViewModel _connectionData;

        private DatabaseTypeEnum _selectedBaseType;

        #endregion

        #region Properties

        public DatabaseTypeEnum SelectedBaseType
        {
            get { return _selectedBaseType; }
            set
            {
                SetProperty(ref _selectedBaseType, value);
                OnDataBaseTypeEnumChanged();
            }
        }

        public BaseConnectionViewModel ConnectionData
        {
            get { return _connectionData; }
            set
            {
                SetProperty(ref _connectionData, value);
            }
        }

        #endregion

        public ConnectionWindowViewModel()
        {
            ConnectionData = new MsSqlConnectionViewModel { CanChange = true };
            DataBaseTypeChanged += ChangeDataContext;
        }

        #region Events

        public event EventHandler DataBaseTypeChanged;

        private void OnDataBaseTypeEnumChanged()
        {
            DataBaseTypeChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private void ChangeDataContext(object sender, EventArgs e)
        {
            switch (_selectedBaseType)
            {

                case DatabaseTypeEnum.MsSql:
                    ConnectionData = new MsSqlConnectionViewModel();
                    break;
                default:
                    ConnectionData = null;
                    break;
            }
        }
    }
}

