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

using System;
using System.Collections.Generic;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Metadata;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseMetadataEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IMetadataDataContext dataContext = DataContextFactory.CreateMetadataDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip MetadataEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IMetadataEndpoint tester = EndpointFactory.CreateMetadataEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateMetadataEndpoint failed", LogType.Error);
                return false;
            }

            bool result = RunTestCase_EncodeDecode(tester);

            List<MetadataItem> metadata = dataContext.GetMetadata();
            if ((metadata == null) || (metadata.Count == 0))
            {
                Logger.AddLog("GetMetadata== 0, tests skipped", LogType.Warning);
            }
            else
            {
                OnBeforeTest("Objects", "MetadataEndpoint", "Metadata Creation");
                // create request
                CreateMetadataRequest requestCreateData = new CreateMetadataRequest
                {
                    entityReferenceType = dataContext.GetEntityReferenceType(),
                    entityUrn = dataContext.GetEntityUrn()
                };
                requestCreateData.MetaDataList.AddRange(metadata);
                CreateMetadataResponse responseCreateData;
                // call endpoint
                MetadataActionResult actionResult = tester.Create(requestCreateData, out responseCreateData);
                result = result && (actionResult == MetadataActionResult.Successful);
                // log response
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseCreateData.ToJSON());
                OnAfterTest();

                OnBeforeTest("Objects", "MetadataEndpoint", "Metadata Deletion");
                // create request
                DeleteMetadataRequest requestDeleteData = new DeleteMetadataRequest
                {
                    entityReferenceType = dataContext.GetEntityReferenceType(),
                    entityUrn = dataContext.GetEntityUrn(),
                    key = metadata[0].key
                };
                DeleteMetadataResponse responseDeleteData;
                // call endpoint
                actionResult = tester.Delete(requestDeleteData, out responseDeleteData);
                result = result && (actionResult == MetadataActionResult.Successful);
                // log response
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseDeleteData.ToJSON());
                OnAfterTest();

                OnBeforeTest("Objects", "MetadataEndpoint", "Lookup Associated Metadata");
                // create request
                LookupMetadataRequest requestLookupData = new LookupMetadataRequest
                {
                    entityReferenceType = dataContext.GetEntityReferenceType(),
                    entityUrn = dataContext.GetEntityUrn(),
                    viewType = dataContext.GetViewType()
                };
                LookupMetadataResponse responseLookupData;
                // call endpoint
                actionResult = tester.Lookup(requestLookupData, out responseLookupData);
                result = result && (actionResult == MetadataActionResult.Successful);
                // log response
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
                OnAfterTest();
            }
            return result;
        }

        protected bool RunTestCase_EncodeDecode(IMetadataEndpoint tester)
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            bool result = true;
            string sValue;

            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding");

            // call endpoint - bool
            bool bValue = true;
            if ((tester.Encode(bValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out bValue) != MetadataActionResult.Successful) ||
                 (true != bValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (bool)" + actionResult);

            // call endpoint - int
            int iValue = 1234;
            if ((tester.Encode(iValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out iValue) != MetadataActionResult.Successful) ||
                 (1234 != iValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (int)" + actionResult);

            // call endpoint - Long
            long lValue = 1234;
            if ((tester.Encode(lValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out lValue) != MetadataActionResult.Successful) ||
                 (1234 != lValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (long)" + actionResult);

            // call endpoint - Float
            float fValue = 12.34f;
            if ((tester.Encode(fValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out fValue) != MetadataActionResult.Successful) ||
                 (12.34f != fValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (float)" + actionResult);

            // call endpoint - Float
            double dValue = 12.34;
            if ((tester.Encode(dValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out dValue) != MetadataActionResult.Successful) ||
                 (12.34 != dValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (double)" + actionResult);

            // call endpoint - DateTime
            DateTime dateValueFix = DateTime.Now;
            DateTime dateValue = dateValueFix;
            if ((tester.Encode(dateValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out dateValue) != MetadataActionResult.Successful) ||
                 (dateValueFix != dateValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (date)" + actionResult);

            // call endpoint - String
            string strValueFix = "123456";
            string strValue = strValueFix;
            if ((tester.Encode(strValue, out sValue) != MetadataActionResult.Successful) ||
                 (tester.Decode(sValue, out strValue) != MetadataActionResult.Successful) ||
                 (strValueFix != strValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: (string)" + actionResult);
            OnAfterTest();

            /*
            OnBeforeTest("Objects", "MetadataEndpoint", "Update an existing user");
            // create request
            MetadataRequest requestUpdateUserData = new MetadataRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                givenName = dataContext.GetGivenName(),
                surname = dataContext.GetSurname() + "_updated",
            };
            MetadataResponse responseUpdateUserData;
            // call endpoint
            actionResult = tester.UpdateUser(requestUpdateUserData, out responseUpdateUserData);
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUpdateUserData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "MetadataEndpoint", "Lookup Specific User by URN");
            UserDataResponse responseUserData;
            // call endpoint
            actionResult = tester.LookupSpecificUser(responseNewUserData.userUrn, dataContext.GetViewType(), out responseUserData);
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUserData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "MetadataEndpoint", "Lookup Specific User by Email Address");
            UserDataResponse responseUserEMailData;
            // call endpoint
            actionResult = tester.LookupSpecificUser(dataContext.GeteMailAddress(), dataContext.GetViewType(), out responseUserEMailData);
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUserEMailData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "MetadataEndpoint", "Change or Reset User Password");
            ChangeOrResetUserPasswordRequest requestPasswordResetData = new ChangeOrResetUserPasswordRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                newPassword = dataContext.GetNewPassword()
            };
            ChangeOrResetUserPasswordResponse responsePasswordResetData;
            // call endpoint
            actionResult = tester.ChangeOrResetUserPassword(requestPasswordResetData, out responsePasswordResetData);
            result = result && (actionResult == MetadataActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responsePasswordResetData.ToJSON());
            OnAfterTest();
            */
            return result;
        }
    }
}