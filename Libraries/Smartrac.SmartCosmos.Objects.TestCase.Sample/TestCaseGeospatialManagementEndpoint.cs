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

using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.TestCase;
using Smartrac.SmartCosmos.Objects.GeospatialManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(80)]
    public class TestCaseGeospatialManagementEndpoint : BaseTestCaseGeospatialManagementEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn geospatialUrn;

            return RunTestCase_Create(out geospatialUrn) &&
                RunTestCase_Update(geospatialUrn) &&
                RunTestCase_Lookup() &&
                RunTestCase_LookupByUrn(geospatialUrn);
        }

        protected virtual bool RunTestCase_Create(out Urn geospatialUrn)
        {
            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Define a geospatial entry");
            // create request
            GeospatialManagementNewRequest requestNewData = new GeospatialManagementNewRequest
            {
                type = dataContext.GetCategory(),
                name = dataContext.GetName(),
                description = dataContext.GetDescription(),
                activeFlag = dataContext.GetActiveFlag(),
                geometricShape = dataContext.GetGeometricShape(),
            };
            GeospatialManagementNewResponse responseNewData;
            // call endpoint
            GeoActionResult actionResult = endpoint.Create(requestNewData, out responseNewData);
            OnAfterTest(actionResult);
            geospatialUrn = (responseNewData == null) ? null : responseNewData.geospatialUrn;
            return (actionResult == GeoActionResult.Successful);
        }

        protected virtual bool RunTestCase_Update(Urn geospatialUrn)
        {
            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Update an existing geospatial entry");
            // create request
            GeospatialManagementUpdateRequest requestUpdateData = new GeospatialManagementUpdateRequest
            {
                geospatialUrn = geospatialUrn, // dataContext.GetGeospatialUrn(),
                name = dataContext.GetName() + "_updated"
            };
            GeospatialManagementUpdateResponse responseUpdateData;
            // call endpoint
            GeoActionResult actionResult = endpoint.Update(requestUpdateData, out responseUpdateData);
            OnAfterTest(actionResult);
            return (actionResult == GeoActionResult.Successful);
        }

        protected virtual bool RunTestCase_Lookup()
        {
            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Lookup Matching Geospatial Entries");
            // create request
            QueryGeospatialEntriesRequest requestQueryData = new QueryGeospatialEntriesRequest
            {
                nameLike = dataContext.GetName(),
                viewType = dataContext.GetViewType()
            };
            QueryGeospatialEntriesResponse responseQueryData;
            // call endpoint
            GeoActionResult actionResult = endpoint.Lookup(requestQueryData, out responseQueryData);
            OnAfterTest(actionResult);
            return (actionResult == GeoActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByUrn(Urn geospatialUrn)
        {
            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Lookup Specific Geospatial Entity by URN");
            GeospatialEntryDataResponse responseSpecificQueryData;
            // call endpoint
            GeoActionResult actionResult = endpoint.Lookup(geospatialUrn, out responseSpecificQueryData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == GeoActionResult.Successful);
        }
    }
}