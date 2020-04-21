using System.Windows.Input;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;

namespace DBManager.Application.ViewModels.MetadataTree
{
    public class TreeSearchViewModel : ViewModelBase
    {
        private readonly TreeViewModel _tree;

        private string _searchText;

        private ICommand _searchCommand;

        public TreeSearchViewModel(TreeViewModel tree)
        {
            _tree = tree;
        }

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public ICommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new RelayCommand(s => SearchExecute(), s => SearchCanExecute()));


        private void SearchExecute()
        {
        }

        private bool SearchCanExecute()
        {
            return !string.IsNullOrEmpty(_searchText);

        }
    }
}
