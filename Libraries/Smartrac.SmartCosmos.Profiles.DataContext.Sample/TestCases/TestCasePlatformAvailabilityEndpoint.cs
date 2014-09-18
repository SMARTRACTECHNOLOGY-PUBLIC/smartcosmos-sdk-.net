using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.SmartCosmos.Profiles.PlatformAvailability;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Profiles.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCasePlatformAvailabilityEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            bool result = true;
            PlatformAvailabilityActionResult actionResult;

            OnBeforeTest("Profiles", "PlatformAvailabilityEndpoint", "Ping");
            // create client for endpoint
            IPlatformAvailabilityEndpoint tester = EndpointFactory.CreatePlatformAvailabilityEndpoint();
            // call endpoint & send response to console
            actionResult = tester.Ping();
            result = result && (actionResult == PlatformAvailabilityActionResult.Successful);

            Logger.AddLog("Result: " + actionResult);
            OnAfterTest();

            return result;
        }
    }
}
