using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.TestCase.Base;
using System.Collections.Generic;


namespace Smartrac.SmartCosmos.Flows.TestCase
{
    [ServiceAttribute(SmartCosmosService.Flows)]
    public class BaseFlowsTestCase<C, E> : BaseTestCase<C, E>
        where C : IBaseDataContext
        where E : IBaseEndpoint
    {
        protected override bool OnBeforeRun()
        {
            return base.OnBeforeRun() && !string.IsNullOrEmpty(EndpointFactory.FlowsServerURL);
        }
    }
}
