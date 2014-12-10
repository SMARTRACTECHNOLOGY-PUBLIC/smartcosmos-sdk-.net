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
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(43)]
    public class TestCaseDeviceEndpoint : BaseTestCaseDeviceEndpoint
    {
        
        
        protected override bool ExecuteTests()
        {
            Urn deviceUrn;
            return TestCreateDevice(out deviceUrn) && 
                TestUpdateDevice() &&
                TestLookupByUrlDevice(deviceUrn) &&
                TestLookupByIdentificationDevice() &&
                TestLookupByNameDevice();
        }

        protected virtual bool TestCreateDevice(out Urn deviceUrn)
        {
            deviceUrn = null;
            OnBeforeTest("Objects", "DeviceEndpoint", "Create");
            // call endpoint
            DeviceDefinitionRequest requestData = new DeviceDefinitionRequest
            {
                identification = dataContext.GetIdentification(),
                name = dataContext.GetName(),
                type = dataContext.GetName()
            };
            DeviceDefinitionResponse responsedData;
            DeviceActionResult actionResult = endpoint.Create(requestData, out responsedData);
            deviceUrn = new Urn(responsedData.message);
            OnAfterTest(actionResult);
            return (actionResult == DeviceActionResult.Successful);
        }

        protected virtual bool TestUpdateDevice()
        {
            OnBeforeTest("Objects", "DeviceEndpoint", "Update");
            // call endpoint
            DeviceUpdateRequest requestData = new DeviceUpdateRequest
            {
                identification = dataContext.GetIdentification(),
                name = dataContext.GetName(),
                type = dataContext.GetType()
            };
            DeviceUpdateResponse responsedData;
            DeviceActionResult actionResult = endpoint.Update(requestData, out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == DeviceActionResult.Successful);
        }

        protected virtual bool TestLookupByIdentificationDevice()
        {
            OnBeforeTest("Objects", "DeviceEndpoint", "LookupByIdentifications");
            // call endpoint
            DeviceLookupResponse responsedData;
            DeviceActionResult actionResult = endpoint.Lookup(new Identification(dataContext.GetIdentification()), out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == DeviceActionResult.Successful);
        }

        protected virtual bool TestLookupByUrlDevice(Urn deviceUrn)
        {
            OnBeforeTest("Objects", "DeviceEndpoint", "LookupByUrl");
            // call endpoint
            DeviceLookupResponse responsedData;
            DeviceActionResult actionResult = endpoint.Lookup(deviceUrn, out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == DeviceActionResult.Successful);
        }

        protected virtual bool TestLookupByNameDevice()
        {
            OnBeforeTest("Objects", "DeviceEndpoint", "LookupByNameDevice");
            // call endpoint
            DeviceLookupsResponse responsedData;
            DeviceActionResult actionResult = endpoint.Lookup(new Name(dataContext.GetName()), out responsedData);
            OnAfterTest(actionResult);
            return (actionResult == DeviceActionResult.Successful);
        }
    }
}