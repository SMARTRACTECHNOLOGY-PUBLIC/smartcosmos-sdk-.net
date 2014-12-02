using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Flows.BusinessRule;
using System;


namespace Smartrac.SmartCosmos.Flows.DataContext.Sample
{
    public class SampleBusinessRuleDataContext : EmptyBusinessRuleDataContext
    {
        public SampleBusinessRuleDataContext()
        {
            urn = new Urn("urn:uuid:a911ab4a-2b3a-4a92-80a9-a9d2de2403b6");
            statusStart = StatusBusinessRule.Start;
            statusStop = StatusBusinessRule.Stop;
            ruleData = new BusinessRuleRequest();
            DateTime thisDay = DateTime.UtcNow;
            ruleData.name = "TestAPI_" + thisDay.ToString("d").Replace('.', '_');
            ruleData.description = "Last test was: " + thisDay.ToString().Replace('.', '_');
        }
    }
}
