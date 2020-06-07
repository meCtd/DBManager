using DBManager.Application.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Application.ViewModels.Windows
{
    class DataGanarationConfigurationViewModel : WindowViewModelBase
    {
        public override string Header => "Data generation configuration";
        public override bool CanUserCloseWindow => true;
    }

}
