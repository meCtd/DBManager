using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.DataGeneration;
using DBManager.Application.ViewModels.General;
using DBManager.Default.DataBaseConnection;
using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.DataGeneration.Core;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Application.ViewModels.Windows
{
    class DataGenerationConfigurationViewModel : WindowViewModelBase
    {
        private DataGenConfig _config;
        private IConnectionData _connection;
        private ICommand _generateCommand;

        public override string Header => "Data generation configuration";

        public int RowCount
        {
            get => _config.RowCount;
            set
            {
               _config.RowCount = value;
                OnPropertyChanged(nameof(RowCount));
            }
        }
        public long Seed
        {
            get => _config.Seed;
            set
            {
                _config.Seed = value;
                OnPropertyChanged(nameof(Seed));
            }
        }
        public ObservableCollection<ViewModelBase> ColumnViewModels { get; }

        public ICommand GenerateCommand => _generateCommand ?? (_generateCommand = new RelayCommand(s => Generate()));

        public DataGenerationConfigurationViewModel(Table selectedItem)
        {
            _config = new DataGenConfig(selectedItem);

            var columns = selectedItem.Children.OfType<Column>();
            ColumnViewModels = new ObservableCollection<ViewModelBase>(columns.Select(col => ConfigurationViewModelFactory.Instance.CreateViewModel(col.DataType.TypeFamilty, col.Name, _config)));
        }

        private void Generate()
        {
            var dataGenerator = new DataGenerator();

            dataGenerator.GenerateData(_config.Scope.GetFirstAncestorOf<Server>().ConnectionData, _config);

            MessageBox.Show($"Successfully generated {_config.RowCount} records in table [{_config.Scope.Name}]");
        }
    }

}
