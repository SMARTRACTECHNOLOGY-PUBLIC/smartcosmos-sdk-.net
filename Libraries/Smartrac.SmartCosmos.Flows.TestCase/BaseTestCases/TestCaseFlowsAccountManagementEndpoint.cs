using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.Flows.AccountManagement;
using Smartrac.SmartCosmos.Flows.DataContext;


namespace Smartrac.SmartCosmos.Flows.TestCase
{
    public class BaseTestCaseFlowsAccountManagementEndpoint : BaseFlowsTestCase<IFlowsAccountManagementDataContext, IFlowsAccountManagementEndpoint>
    {
        protected override IFlowsAccountManagementDataContext CreateDataContext()
        {
            return DataContextFactory.CreateFlowsAccountManagementDataContext();
        }

        protected override IFlowsAccountManagementEndpoint CreateEndpoint()
        {
            return EndpointFactory.CreateFlowsAccountManagementEndpoint();
        }
    }
}
