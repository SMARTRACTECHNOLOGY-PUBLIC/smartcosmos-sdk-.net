using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.AccountManager.Base;
using System;

namespace Smartrac.SmartCosmos.AccountManager.DataContext.Sample
{
    public static class MyDataContext
    {
       
        public static string GetName()
        {
            return "testSDK";
        }

        public static Email GetEmail()
        {
            return new Email("testSDK@test.sdk");
        }

        public static Urn GetRoleUrn()
        {
            return new Urn("urn:uuid:0ac50422-ba9b-4797-b5b1-bdbc32e13680");
        }

        public static string GetSurname()
        {
            return "SDK";
        }

        public static bool GetEnabled()
        {
            return true;
        }

        public static string GetGivenName()
        {
            return "TestSDK";
        }
    }
}