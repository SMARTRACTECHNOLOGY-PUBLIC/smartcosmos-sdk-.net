using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;

namespace Smartrac.SmartCosmos.AccountManager.DataContext.Sample
{
    public class SampleRoleDataContext : BaseRoleDataContext
    {
        
       public override string GetName()
        {
            return "TestSDK";
        }
    }
}