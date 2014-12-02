using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartrac.SmartCosmos.Flows.DataContext
{
    public class BaseFlowsAccountManagementDataContext : IFlowsAccountManagementDataContext
    {
        public virtual string GeteMailAddress()
        {
            return "";
        }
    }
}
