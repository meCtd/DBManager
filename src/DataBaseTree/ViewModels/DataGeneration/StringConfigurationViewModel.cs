using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBManager.Application.ViewModels.General;
using DBManager.Default.DataGeneration.Configuration;

namespace DBManager.Application.ViewModels.DataGeneration
{
    class StringConfigurationViewModel : ColumnConfigurationViewModelBase
    {
        public StringConfigurationViewModel(string columnName, DataGenConfig config) 
            : base(columnName,config)
        {
        }

        protected override void FillColumnConfigProperties(DataGenerationColumnConfig config)
        {

        }
    }
}
