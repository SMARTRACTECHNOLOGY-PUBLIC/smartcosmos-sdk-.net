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

#endregion License

using Smartrac.SmartCosmos.Profiles.TagVerification;
using Smartrac.SmartCosmos.TestCase.Base;
using System.Linq;

namespace Smartrac.SmartCosmos.Profiles.TestCase.Sample
{
    [TestCaseAttribute(10)]
    public class TestCaseTagVerificationEndpoint : BaseTestCaseTagVerificationEndpoint
    {
        protected override bool ExecuteTests()
        {
            return TestVerifyTags() &&
                   TestGetVerificationMessage() &&
                   TestVerifyTagsForRoundRockCompliance();
        }

        protected virtual bool TestVerifyTags()
        {
            TagVerificationActionResult actionResult = TagVerificationActionResult.Failed;
            foreach (var verificationItem in dataContext.GetVerificationTypes())
            {
                OnBeforeTest("Profiles", "TagVerificationEndpoint", "VerifyTags " + verificationItem);
                // create request
                VerifyTagsRequest requestVerifyTags = new VerifyTagsRequest(dataContext);
                requestVerifyTags.verificationType = verificationItem;
                // call endpoint
                VerifyTagsResponse responseVerifyTags;
                actionResult = endpoint.VerifyTags(requestVerifyTags, out responseVerifyTags);
                // log response
                OnAfterTest(actionResult);
            }
            return (actionResult == TagVerificationActionResult.Successful);
        }

        protected virtual bool TestVerifyTagsForRoundRockCompliance()
        {
            OnBeforeTest("Profiles", "TagVerificationEndpoint", "VerifyTagsForRoundRockCompliance");
            // create request
            VerifyTagsRequestRR requestVerifyTags = new VerifyTagsRequestRR(dataContext);
            // call endpoint
            VerifyTagsResponse responseVerifyTags;
            TagVerificationActionResult actionResult = endpoint.VerifyTagsForRoundRockCompliance(requestVerifyTags, out responseVerifyTags);
            // log response
            OnAfterTest(actionResult);

            return (actionResult == TagVerificationActionResult.Successful);
        }

        protected virtual bool TestGetVerificationMessage()
        {
            OnBeforeTest("Profiles", "TagVerificationEndpoint", "GetVerificationMessage");
            // create request
            VerificationMessageRequest requestVerificationMessage = new VerificationMessageRequest();
            requestVerificationMessage.verificationState = 0;
            requestVerificationMessage.verificationType = dataContext.GetVerificationTypes().First<string>();
            VerificationMessageResponse responseVerificationMessage;
            // call endpoint
            TagVerificationActionResult actionResult = endpoint.GetVerificationMessage(requestVerificationMessage, out responseVerificationMessage);
            // log response
            OnAfterTest(actionResult);

            return (actionResult == TagVerificationActionResult.Successful);
        }
    }
}