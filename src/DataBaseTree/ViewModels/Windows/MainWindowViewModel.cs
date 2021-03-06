﻿using System.Linq;
using System.Windows.Input;

using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Application.ViewModels.MetadataTree;
using DBManager.Application.ViewModels.MetadataTree.TreeItems;

using DBManager.Default;
using DBManager.Default.Tree.DbEntities;

using Framework.EventArguments;

using Ninject;


namespace DBManager.Application.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand _disconnectCommand;

        public TreeViewModel Tree { get; } = new TreeViewModel();

        public TreeSearchViewModel TreeSearch { get; }

        private RelayCommand _connectCommand;

        public ScriptViewModel Script { get; } = new ScriptViewModel();

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand(s => Connect()));

        public ICommand DisconnectCommand => _disconnectCommand ?? (_disconnectCommand = new RelayCommand((s) => { }, s => Tree.RootItems.Count > 0));

        private void Connect()
        {
            var viewModel = new ConnectionWindowViewModel();
            viewModel.Connected += OnNewSourceConnected;

            Context.Resolver.Get<IWindowManager>().ShowWindow(viewModel);
        }

        private void OnNewSourceConnected(object sender, ArgumentEventArgs<(DialectType Dialect, string Name)> e)
        {
            if (Tree.RootItems.Cast<MetadataViewModelBase>().Any(s => s.Name.Equals(e.Argument.Name)))
                return;

            Tree.RootItems.Add(new ServerViewModel(new Server(e.Argument.Name, e.Argument.Dialect)));
        }
    }
}
