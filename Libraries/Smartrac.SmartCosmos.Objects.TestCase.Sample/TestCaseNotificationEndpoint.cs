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

using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Device;
using Smartrac.SmartCosmos.Objects.Notification;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(1)]
    public class TestCaseNotificationEndpoint : BaseTestCaseNotificationEndpoint
    {        
        
        protected override bool ExecuteTests()
        {
            Urn notificationUrn;
            return TestCreateNotification(out notificationUrn) &&
                TestConfirmNotification() &&
                TestLookupNotificationByURN(notificationUrn) &&
                TestWithdrawNotification(notificationUrn);
        }

        protected virtual bool TestCreateNotification(out Urn notificationUrn)
        {
            notificationUrn = null;
            OnBeforeTest("Objects", "NotificationEndpoint", "TestCreateNotification");
            // call endpoint
            EnrollNotificationsRequest requestData = new EnrollNotificationsRequest
            {
                endpointUrl = "http://vbond.kiev.ua/smartrac/NotificationEndpoints.php",
                name = "Test",
                description = "About test"
            };
            NotificationBaseResponse responsedData;
            NotificationActionResult actionResult = endpoint.Create(requestData, out responsedData);
            notificationUrn = new Urn(responsedData.message);
            OnAfterTest(actionResult);
            return (actionResult == NotificationActionResult.Successful);
        }

        protected virtual bool TestConfirmNotification()
        {
            OnBeforeTest("Objects", "NotificationEndpoint", "TestConfirmNotification");
            // call endpoint
            EnrollmentConfirmationRequest requestData = new EnrollmentConfirmationRequest
            {
                topicArn = "Test",
                token = "About test"
            };
            NotificationBaseResponse responsedData;
            NotificationActionResult actionResult = endpoint.Confirm(requestData, out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == NotificationActionResult.Successful);
        }

        protected virtual bool TestWithdrawNotification(Urn notificationUrn)
        {
            OnBeforeTest("Objects", "NotificationEndpoint", "TestWithdrawNotification");
            // call endpoint            
            NotificationBaseResponse responsedData;
            NotificationActionResult actionResult = endpoint.Delete(notificationUrn, out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == NotificationActionResult.Successful);
        }

        protected virtual bool TestLookupNotificationByURN(Urn notificationUrn)
        {
            OnBeforeTest("Objects", "NotificationEndpoint", "TestLookupNotificationByURN");
            // call endpoint
            LookupNotificationsResponse responsedData;
            NotificationActionResult actionResult = endpoint.Lookup(notificationUrn, out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == NotificationActionResult.Successful);
        }        
    }
}