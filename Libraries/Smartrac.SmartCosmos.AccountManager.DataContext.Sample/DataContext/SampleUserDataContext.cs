using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;


namespace Smartrac.SmartCosmos.AccountManager.DataContext.Sample
{
    public class SampleUserDataContext : BaseUserDataContext
    {
        public override string GetGivenName()
        {
            return "testSDK";
        }

       public override bool GetEnabled()
        {
            return true;
        }

        public override string GetSurname()
        {
            return "SDK";
        }

        public override Email GetEmail()
        {
            return new Email("api.tester@test.com");
        }

        public override Urn GetRoleUrn()
        {
            return new Urn("urn:uuid:0ac50422-ba9b-4797-b5b1-bdbc32e13680");
        }
    }
}
