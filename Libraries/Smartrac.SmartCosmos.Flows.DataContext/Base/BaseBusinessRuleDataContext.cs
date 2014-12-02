using Smartrac.SmartCosmos.Flows.BusinessRule;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.Flows.DataContext
{
    public class BaseBusinessRuleDataContext : IBusinessRuleDataContext
    {
        public virtual Urn GetUrn()
        {
            return new Urn("");
        }

        public virtual StatusBusinessRule GetStatusStart()
        {
            return StatusBusinessRule.Start;
        }

        public virtual StatusBusinessRule GetStatusStop()
        {
            return StatusBusinessRule.Stop;
        }

        public virtual BusinessRuleRequest GetRuleData()
        {
            return new BusinessRuleRequest();
        }
    }
}
