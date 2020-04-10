using System;
using System.Linq;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;
using DBManager.Default.Tree.DbEntities;
using Framework.EventArguments;

using Ninject;

namespace DBManager.Application.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TreeViewModel Tree { get; } = new TreeViewModel();

        public TreeSearchViewModel TreeSearch { get; }

        private RelayCommand _connectCommand;

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand(s => Connect()));

        private void Connect()
        {
            var viewModel = new ConnectionWindowViewModel();
            viewModel.Connected += OnNewSourceConnected;

            Resolver.Get<IWindowManager>().ShowWindow(viewModel);
        }

        private void OnNewSourceConnected(object sender, ArgumentEventArgs<string> e)
        {
            if (Tree.RootItems.Cast<MetadataViewModelBase>().Any(s => s.ObjectName.Equals(e.Argument)))
                return;

            Tree.RootItems.Add(new DbObjectViewModel(null,new Server(e.Argument)));
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