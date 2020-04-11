using DBManager.Application.ViewModels.General;
using DBManager.Default;

using ICSharpCode.AvalonEdit.Highlighting;

namespace DBManager.Application.ViewModels
{
    public class ScriptViewModel : ViewModelBase
    {
        private string _sql;

        public IHighlightingDefinition Highlighting => HighlightingManager.Instance.GetDefinition(DialectType.SqlServer.ToString());

        public string Sql
        {
            get => _sql;
            set => SetProperty(ref _sql, value);
        }
        
    }
}
