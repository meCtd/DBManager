﻿using System;
using System.Windows.Input;

namespace DBManager.Application.Framework
{
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute((T)parameter);
		}

		public void Execute(object parameter)
		{
			_execute((T)parameter);
		}
	}
}
