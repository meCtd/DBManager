using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree;


namespace DBManager.Application.ViewModels.Windows
{
    public class TreeWindowViewModel : ViewModelBase
    {
        public TreeViewModel Tree { get; } = new TreeViewModel();

        public ScriptsViewModel Scripts { get; } = new ScriptsViewModel();

        public ConnectionManagerViewModel ConnectionManager { get; }

        public TreeWindowViewModel()
        {
            ConnectionManager = new ConnectionManagerViewModel(Tree);
        }

    }
}
