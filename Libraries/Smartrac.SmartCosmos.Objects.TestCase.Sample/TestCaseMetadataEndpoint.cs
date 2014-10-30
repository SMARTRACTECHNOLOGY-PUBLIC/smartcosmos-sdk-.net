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
using Smartrac.SmartCosmos.Objects.TestCase;
using Smartrac.SmartCosmos.Objects.Metadata;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(90)]
    public class TestCaseMetadataEndpoint : BaseTestCaseMetadataEndpoint
    {
        protected override bool ExecuteTests()
        {
            List<MetadataItem> metadata = dataContext.GetMetadata();
            if ((metadata == null) || (metadata.Count == 0))
            {
                Logger.AddLog("GetMetadata== 0, some tests skipped", LogType.Info);
            }

            return RunTestCase_EncodeDecode_Bool() &&
                   RunTestCase_EncodeDecode_Integer() &&
                   RunTestCase_EncodeDecode_Long() &&
                   RunTestCase_EncodeDecode_Float() &&
                   RunTestCase_EncodeDecode_Double() &&
                   RunTestCase_EncodeDecode_DateTime() &&
                   RunTestCase_EncodeDecode_String() &&
                   RunTestCase_Create(metadata) &&
                   RunTestCase_Lookup(metadata) &&
                   RunTestCase_Delete(metadata);
        }

        protected virtual bool RunTestCase_Create(List<MetadataItem> metadata)
        {
            if ((metadata == null) || (metadata.Count == 0))
                return true;

            OnBeforeTest("Objects", "MetadataEndpoint", "Metadata Creation");
            // create request
            CreateMetadataRequest requestCreateData = new CreateMetadataRequest
            {
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrn = dataContext.GetEntityUrn()
            };
            requestCreateData.MetaDataList.AddRange(metadata);
            CreateMetadataResponse responseCreateData;
            // call endpoint
            MetadataActionResult actionResult = endpoint.Create(requestCreateData, out responseCreateData);
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected virtual bool RunTestCase_Delete(List<MetadataItem> metadata)
        {
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
            MetadataActionResult actionResult = endpoint.Delete(requestDeleteData, out responseDeleteData);
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected virtual bool RunTestCase_Lookup(List<MetadataItem> metadata)
        {
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
            MetadataActionResult actionResult = endpoint.Lookup(requestLookupData, out responseLookupData);
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_Bool()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;

            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - Boolean");

            // call endpoint - bool
            bool bValue = true;
            if ((endpoint.Encode(bValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out bValue) != MetadataActionResult.Successful) ||
                 (true != bValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_Integer()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;
            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - Integer");
            // call endpoint - int
            int iValue = 1234;
            if ((endpoint.Encode(iValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out iValue) != MetadataActionResult.Successful) ||
                 (1234 != iValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_Long()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;
            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - Long");
            // call endpoint - Long
            long lValue = 1234;
            if ((endpoint.Encode(lValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out lValue) != MetadataActionResult.Successful) ||
                 (1234 != lValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_Float()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;

            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - Float");
            // call endpoint - Float
            float fValue = 12.34f;
            if ((endpoint.Encode(fValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out fValue) != MetadataActionResult.Successful) ||
                 (12.34f != fValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_Double()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;

            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - Double");
            // call endpoint - Float
            double dValue = 12.34;
            if ((endpoint.Encode(dValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out dValue) != MetadataActionResult.Successful) ||
                 (12.34 != dValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_DateTime()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;

            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - DateTime");
            // call endpoint - DateTime
            DateTime dateValueFix = DateTime.UtcNow;
            DateTime dateValue = dateValueFix;
            if ((endpoint.Encode(dateValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out dateValue) != MetadataActionResult.Successful) ||
                 (dateValueFix != dateValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful);
        }

        protected bool RunTestCase_EncodeDecode_String()
        {
            MetadataActionResult actionResult = MetadataActionResult.Successful;
            string sValue;

            OnBeforeTest("Objects", "MetadataEndpoint", "Type-Safe Encoding / Decoding - String");
            // call endpoint - String
            string strValueFix = "123456";
            string strValue = strValueFix;
            if ((endpoint.Encode(strValue, out sValue) != MetadataActionResult.Successful) ||
                 (endpoint.Decode(sValue, out strValue) != MetadataActionResult.Successful) ||
                 (strValueFix != strValue))
            {
                actionResult = MetadataActionResult.Failed;
            }
            OnAfterTest(actionResult);
            return (actionResult == MetadataActionResult.Successful); ;
        }
    }
}