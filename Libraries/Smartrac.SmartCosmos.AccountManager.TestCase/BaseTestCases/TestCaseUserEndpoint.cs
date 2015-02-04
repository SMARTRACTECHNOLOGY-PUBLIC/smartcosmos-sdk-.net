using Smartrac.SmartCosmos.AccountManager.DataContext;
using Smartrac.SmartCosmos.AccountManager.User;
using System;

namespace Smartrac.SmartCosmos.AccountManager.TestCase
{
    public class BaseTestCaseUserEndpoint : BaseAccountManagerTestCase<IUserDataContext, IUserEndpoint>
    {
        protected override IUserDataContext CreateDataContext()
        {
            return DataContextFactory.CreateUserDataContext();
        }

        protected override IUserEndpoint CreateEndpoint()
        {
            return EndpointFactory.CreateUserEndpoint();
        }
    }
}
