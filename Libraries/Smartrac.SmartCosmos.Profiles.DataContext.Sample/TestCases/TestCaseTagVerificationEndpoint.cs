#region License
// SMART COSMOS .Net SDK
// (C) Copyright 2014 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Profiles.TagVerification;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Profiles.DataContext.Sample
{
    [TestCaseAttribute]
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
