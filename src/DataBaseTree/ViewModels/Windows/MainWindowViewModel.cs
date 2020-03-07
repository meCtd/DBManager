using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.View.Windows;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree;

namespace DBManager.Application.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TreeViewModel Tree { get; } = new TreeViewModel();

        public TreeSearchViewModel TreeSearch { get; }


        private string _definitionText;

        private bool _isSaveInProcess;


        private RelayCommand _connectCommand;

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand(s => Connect()));

        private void Connect()
        {
            var window = new ConnectionWindow();
            var data = (ConnectionWindowViewModel)window.DataContext;

            if (window.ShowDialog() == true)
            {
            }
        }


        private ICommand _disconnectCommand;

        public ICommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ?? (_disconnectCommand = new RelayCommand(
                           (s) => { }, s => Tree.RootItems.Count > 0));
            }
        }
    }
}