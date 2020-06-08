using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.DataGeneration;
using DBManager.Application.ViewModels.General;
using DBManager.Default.DataGeneration.Configuration;
using DBManager.Default.Tree.DbEntities;

namespace DBManager.Application.ViewModels.Windows
{
    class DataGenerationConfigurationViewModel : WindowViewModelBase
    {
        private DataGenConfig _config = new DataGenConfig();
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
            var columns = selectedItem.Children.OfType<Column>();
            ColumnViewModels = new ObservableCollection<ViewModelBase>(columns.Select(col => ConfigurationViewModelFactory.Instance.CreateViewModel(col.DataType.TypeFamilty, col.Name, _config)));
        }

        private void Generate()
        {
            throw new NotImplementedException();
        }
    }

}
