using Smartrac.SmartCosmos.AccountManager.DataContext;
using Smartrac.SmartCosmos.AccountManager.Role;
using System;

namespace Smartrac.SmartCosmos.AccountManager.TestCase
{
    public class BaseTestCaseRoleEndpoint : BaseAccountManagerTestCase<IRoleDataContext, IRoleEndpoint>
    {
        protected override IRoleDataContext CreateDataContext()
        {
            return DataContextFactory.CreateRoleDataContext();
        }

        protected override IRoleEndpoint CreateEndpoint()
        {
            return EndpointFactory.CreateRoleEndpoint();
        }
    }
}
