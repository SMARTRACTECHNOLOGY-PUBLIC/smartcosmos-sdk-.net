using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    public static class MyDataContext
    {
        private static int randomId = new Random().Next(0, 100000000);

        public static string GetRealm()
        {
            return "yourarea" + randomId + ".com";
        }

        public static string GeteMailAddress()
        {
            return "testuser@yourarea" + randomId + ".com";
        }

        public static Urn GetSampleObjectUrn()
        {
            return new Urn("urn:building:mall:ParadiseValley:2");
        }
    }
}