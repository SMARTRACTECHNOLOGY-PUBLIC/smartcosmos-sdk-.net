using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Flows.Base;
using Smartrac.SmartCosmos.Flows.BusinessRule;

namespace Smartrac.SmartCosmos.Flows.DataContext
{
    public interface IBusinessRuleDataContext : IBaseDataContext
    {
        Urn GetUrn();

        StatusBusinessRule GetStatusStart();

        StatusBusinessRule GetStatusStop();

        BusinessRuleRequest GetRuleData();
    }
}
