using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Flows.Base;

namespace Smartrac.SmartCosmos.Flows.DataContext
{
    public interface IFlowsAccountManagementDataContext : IBaseDataContext
    {
        string GeteMailAddress();
    }
}
