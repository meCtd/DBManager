using System.Collections.ObjectModel;
using DBManager.Application.ViewModels.General;
using DBManager.Default;

using ICSharpCode.AvalonEdit.Highlighting;

namespace DBManager.Application.ViewModels
{
    public class ScriptsViewModel : ViewModelBase
    {
        public ObservableCollection<ScriptViewModel> Tabs { get; } = new ObservableCollection<ScriptViewModel>();

        private ScriptViewModel _selected;

        public ScriptViewModel Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }

    public class ScriptViewModel : ViewModelBase
    {
        private string _sql;

        public IHighlightingDefinition Highlighting { get; }

        public string Sql
        {
            get => _sql;
            set => SetProperty(ref _sql, value);
        }

        public string Name { get; }

        public ScriptViewModel(string name, DialectType dialect)
        {
            Name = name;
            Highlighting = HighlightingManager.Instance.GetDefinition(dialect.ToString());
        }


    }
}
