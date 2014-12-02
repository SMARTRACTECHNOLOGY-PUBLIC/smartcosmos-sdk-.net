using Smartrac.SmartCosmos.Flows.BusinessRule;
using Smartrac.SmartCosmos.Flows.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.Flows.TestCase
{
    public class BaseTestCaseBusinessRuleEndpoint : BaseFlowsTestCase<IBusinessRuleDataContext, IBusinessRuleEndpoint>
    {
        protected override IBusinessRuleDataContext CreateDataContext()
        {
            return DataContextFactory.CreateBusinessRuleDataContext();
        }

        protected override IBusinessRuleEndpoint CreateEndpoint()
        {
            return EndpointFactory.CreateBusinessRuleEndpoint();
        }
    }
}
