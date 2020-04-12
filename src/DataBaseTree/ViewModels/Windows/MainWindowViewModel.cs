using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree;


namespace DBManager.Application.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TreeViewModel Tree { get; } = new TreeViewModel();

        public ScriptsViewModel Scripts { get; } = new ScriptsViewModel();

        public ConnectionManagerViewModel ConnectionManager { get; }

        public MainWindowViewModel()
        {
            ConnectionManager = new ConnectionManagerViewModel(Tree);
        }

    }
}
