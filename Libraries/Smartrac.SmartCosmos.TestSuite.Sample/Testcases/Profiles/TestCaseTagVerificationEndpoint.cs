using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Profiles.TagVerification;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    [TestSuiteAttribute]
    public class TestCaseTagVerificationEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            ITagDataContext dataContext = DataContextFactory.CreateTagDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip test cases for TagVerificationEndpoint, because of missing data context", LogType.Info);
                return true;
            }

            ITagVerificationEndpoint tester = EndpointFactory.CreateTagVerificationEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateTagVerificationEndpoint failed", LogType.Error);
                return false;
            }            
            
            bool result = true;

            OnBeforeTest("Profiles", "TagVerificationEndpoint", "VerifyTags");
            // create request
            VerifyTagsRequest requestVerifyTags = new VerifyTagsRequest(dataContext);
            // call endpoint
            VerifyTagsResponse responseVerifyTags;
            TagVerificationActionResult actionResult = tester.VerifyTags(requestVerifyTags, out responseVerifyTags);
            result = (actionResult == TagVerificationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseVerifyTags.ToJSON());
            OnAfterTest();

            OnBeforeTest("Profiles", "TagVerificationEndpoint", "GetVerificationMessage");
            // create request
            VerificationMessageRequest requestVerificationMessage = new VerificationMessageRequest();
            requestVerificationMessage.verificationState = 0;
            requestVerificationMessage.verificationType = dataContext.GetVerificationTypes().First<string>();
            VerificationMessageResponse responseVerificationMessage;
            // call endpoint
            actionResult = tester.GetVerificationMessage(requestVerificationMessage, out responseVerificationMessage);
            result = result && (actionResult == TagVerificationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseVerificationMessage.ToJSON());
            OnAfterTest();

            return result;  
        }
    }
}
