using System.Collections.ObjectModel;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;

namespace DBManager.Application.ViewModels
{
    public class ScriptsViewModel : ViewModelBase
    {
        private const string FileNameFormat = "SQLQuery{0}.sql";

        private static int _counter = 1;

        private ICommand _closeTabCommand;
        private ICommand _newTabCommand;

        private ScriptExecutorViewModel _selectedTab;

        public ObservableCollection<ScriptExecutorViewModel> Tabs { get; } = new ObservableCollection<ScriptExecutorViewModel>();

        public ScriptExecutorViewModel SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        public ICommand CloseTabCommand => _closeTabCommand ?? (_closeTabCommand =
                                              new RelayCommand<ScriptExecutorViewModel>(s =>
                                              {
                                                  if (s.Close())
                                                      Tabs.Remove(s);

                                              }, s => Tabs.Count > 0));

        public ICommand NewTabCommand => _newTabCommand ?? (_newTabCommand =
            new RelayCommand<MetadataViewModelBase>(s =>
            {
                var tab = new ScriptExecutorViewModel(GenerateName(), s.Root);
                Tabs.Add(tab);

                SelectedTab = tab;
            }, s => s != null));


        private string GenerateName()
        {
            var name = string.Format(FileNameFormat, _counter);
            _counter++;
            return name;
        }

    }
}
