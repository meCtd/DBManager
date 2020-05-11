using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Application.Utils;
using DBManager.Application.ViewModels.General;
using DBManager.Default.Execution;
using Framework.Extensions;
using Ninject;


namespace DBManager.Application.ViewModels.ScriptExecution
{
    public class ScriptResultViewModel : ViewModelBase
    {
        private bool _isBusy;

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool HasData => Data.Count > 0;

        public bool IsEmpty => !HasData && string.IsNullOrEmpty(Message);

        public ObservableCollection<DataTable> Data { get; } = new ObservableCollection<DataTable>();

        public async void Fill(IScriptExecutionResult result)
        {
            Reset();

            var builder = new StringBuilder();
            result.Info.StatementAffectedRows.ForEach(s => builder.AppendLine($"({s} row(s) affected)\n"));
            builder.Append($"Completion time : {result.Info.ExecutionTime}");

            Message = builder.ToString();

            using (var reader = result.Reader)
            {
                do
                {
                    var table = new DataTable();
                    await Task.Run(() => table.Load(reader.Instance));

                    Context.Resolver.Get<IWindowManager>().RunOnUi(() =>
                    {
                        Data.Add(table);
                    });

                } while (!reader.Instance.IsClosed);
            }
            RefreshAllBindings();
        }

        public void Fill(Exception e)
        {
            Reset();
            Message = e.InnerException?.Message ?? e.Message;
            RefreshAllBindings();
        }


        private void Reset()
        {
            Data.Clear();
            Message = string.Empty;
        }
    }
}
