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
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.GeospatialManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseGeospatialManagementEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IGeospatialManagementDataContext dataContext = DataContextFactory.CreateGeospatialManagementDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip GeospatialManagementEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IGeospatialManagementEndpoint tester = EndpointFactory.CreateGeospatialManagementEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateGeospatialManagementEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            GeospatialManagementActionResult actionResult;

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Define a geospatial entry");
            // create request         
            GeospatialManagementNewRequest requestNewData = new GeospatialManagementNewRequest
            {
                geospatialUrn = dataContext.GetGeospatialUrn(),
                type = dataContext.GetType(),
                name = dataContext.GetName(),
                description = dataContext.GetDescription(),
                activeFlag = dataContext.GetActiveFlag(),
                geometricShape = dataContext.GetGeometricShape(),
            };
            GeospatialManagementNewResponse responseNewData;
            // call endpoint  
            actionResult = tester.CreateNewGeospatial(requestNewData, out responseNewData);
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewData.ToJSON());
            OnAfterTest();

            if((responseNewData == null) || (! responseNewData.geospatialUrn.IsValid()))
            {
               Logger.AddLog("Abort testing");
               return false;
            }

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Update an existing geospatial entry");
            // create request
            GeospatialManagementUpdateRequest requestUpdateData = new GeospatialManagementUpdateRequest
            {
                geospatialUrn = responseNewData.geospatialUrn, // dataContext.GetGeospatialUrn(),
                name = dataContext.GetName() + "_updated"
            };
            GeospatialManagementUpdateResponse responseUpdateData;
            // call endpoint  
            actionResult = tester.UpdateGeospatial(requestUpdateData, out responseUpdateData);
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUpdateData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Lookup Matching Geospatial Entries");
            // create request
            QueryGeospatialEntriesRequest requestQueryData = new QueryGeospatialEntriesRequest
            {
                nameLike = dataContext.GetName(),
                viewType = dataContext.GetViewType()
            };
            QueryGeospatialEntriesResponse responseQueryData;
            // call endpoint  
            actionResult = tester.LookupMatchingGeospatialEntries(requestQueryData, out responseQueryData);
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQueryData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Lookup Specific Geospatial Entity by URN");
            GeospatialEntryDataResponse responseSpecificQueryData;
            // call endpoint  
            actionResult = tester.LookupSpecificGeospatialEntitybyURN(responseNewData.geospatialUrn, out responseSpecificQueryData, dataContext.GetViewType());
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseSpecificQueryData.ToJSON());
            OnAfterTest();
                       

            return result;
        }
    }
}
