using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using DBManager.Application.ViewModels.General;


namespace DBManager.Application.ViewModels.ScriptExecution
{
    public class ScriptResultViewModel : ViewModelBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool HasData => (Data?.Count ?? 0) > 0;

        public bool IsEmpty => !HasData && string.IsNullOrEmpty(Message);

        public ObservableCollection<DataTable> Data { get; }

        public ScriptResultViewModel(IEnumerable<DataTable> data, string message)
        {
            Data = new ObservableCollection<DataTable>(data);
            Message = message;
        }

        public ScriptResultViewModel(Exception exception)
        {
            Message = exception.InnerException?.Message ?? exception.Message;
        }
    }
}
