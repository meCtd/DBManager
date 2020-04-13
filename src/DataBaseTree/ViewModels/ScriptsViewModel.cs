using System.Collections.ObjectModel;
using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;

using DBManager.Default;


namespace DBManager.Application.ViewModels
{
    public class ScriptsViewModel : ViewModelBase
    {
        private ICommand _closeTabCommand;

        private ScriptViewModel _selected;

        public ObservableCollection<ScriptViewModel> Tabs { get; } = new ObservableCollection<ScriptViewModel>();

        public ScriptViewModel Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public ICommand CloseTabCommand => _closeTabCommand ?? (_closeTabCommand =
                                               new RelayCommand<ScriptViewModel>(s => Tabs.Remove(s), s => Tabs.Count > 0));

        public ScriptsViewModel()
        {
            Tabs.Add(new ScriptViewModel("Test", DialectType.SqlServer));
        }
    }
}
