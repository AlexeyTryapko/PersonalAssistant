using PersonalAssistantWCF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Strategy.Interface
{
    interface IExport
    {
        void ExportData(User user);
    }
}
