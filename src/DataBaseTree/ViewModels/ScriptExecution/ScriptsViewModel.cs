using System.Collections.ObjectModel;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default;
using Ninject;

namespace DBManager.Application.ViewModels.ScriptExecution
{
    public class ScriptsViewModel : ViewModelBase
    {
        private const string FileNameFormat = "SQLQuery{0}.sql";

        private static int _counter = 1;

        private ICommand _closeTabCommand;
        private ICommand _newTabCommand;
        private ICommand _selectTopRowCommand;

        private ScriptViewModel _selectedTab;

        public ObservableCollection<ScriptViewModel> Tabs { get; } = new ObservableCollection<ScriptViewModel>();

        public ScriptViewModel SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        public ICommand CloseTabCommand => _closeTabCommand ?? (_closeTabCommand =
                                              new RelayCommand<ScriptViewModel>(s =>
                                              {
                                                  if (s.Close())
                                                      Tabs.Remove(s);

                                              }, s => Tabs.Count > 0));

        public ICommand NewTabCommand => _newTabCommand ?? (_newTabCommand =
            new RelayCommand<MetadataViewModelBase>(s =>
            {
                var tab = new ScriptViewModel(GenerateName(), s.Root);
                Tabs.Add(tab);

                SelectedTab = tab;
            }, s => s != null));

        public ICommand SelectTopRowsCommand => _selectTopRowCommand ?? (_selectTopRowCommand =
            new RelayCommand<MetadataViewModelBase>(s =>
            {
                NewTabCommand.Execute(s);
                
                var printer = Context.Resolver.Get<IDialectComponent>(s.Dialect.ToString()).Printer;

                SelectedTab.Sql = printer.GetTop100RowsQuery(((DbObjectViewModel)s).Model);
                SelectedTab.ExecutionContext = null;
                SelectedTab.ExecuteCommand.Execute(new object());
            }, s => s != null));


        private string GenerateName()
        {
            var name = string.Format(FileNameFormat, _counter);
            _counter++;
            return name;
        }

    }
}
