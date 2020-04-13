using System.Collections.ObjectModel;
using DBManager.Application.ViewModels.General;
using DBManager.Default;

using ICSharpCode.AvalonEdit.Highlighting;

namespace DBManager.Application.ViewModels
{
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

        public DialectType Dialect { get; }

        public ScriptViewModel(string name, DialectType dialect)
        {
            Name = name;
            Dialect = dialect;
            Highlighting = HighlightingManager.Instance.GetDefinition(dialect.ToString());
        }
    }
}
