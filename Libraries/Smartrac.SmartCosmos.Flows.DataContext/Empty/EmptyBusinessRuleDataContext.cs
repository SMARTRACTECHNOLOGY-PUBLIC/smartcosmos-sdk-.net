using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Flows.Base;
using Smartrac.SmartCosmos.Flows.BusinessRule;
using Smartrac.SmartCosmos.Flows.DataContext;

namespace Smartrac.SmartCosmos.Flows.DataContext
{
    public class EmptyBusinessRuleDataContext : BaseBusinessRuleDataContext
    {
        public Urn urn { get; set; }

        public StatusBusinessRule statusStart { get; set; }

        public StatusBusinessRule statusStop { get; set; }

        public BusinessRuleRequest ruleData { get; set; }

        public override Urn GetUrn()
        {
            return urn;
        }

        public override StatusBusinessRule GetStatusStart()
        {
            return statusStart;
        }

        public override StatusBusinessRule GetStatusStop()
        {
            return statusStop;
        }

        public override BusinessRuleRequest GetRuleData()
        {
            return ruleData;
        }
    }
}
