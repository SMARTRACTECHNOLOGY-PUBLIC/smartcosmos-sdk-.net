using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.GeospatialManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    [TestSuiteAttribute]
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

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Create new object");
            // create request         
            GeospatialManagementNewRequest requestNewObjectData = new GeospatialManagementNewRequest
            {
                urn = dataContext.GetObjectUrn(),
                type = dataContext.GetType(),
                name = dataContext.GetName(),
                description = dataContext.GetDescription(),
                activeFlag = dataContext.GetActiveFlag()
            };
            GeospatialManagementResponse responseNewObjectData;
            // call endpoint  
            actionResult = tester.CreateNewObject(requestNewObjectData, out responseNewObjectData);
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Update an existing Object");
            // create request
            GeospatialManagementRequest requestUpdateObjectData = new GeospatialManagementRequest
            {
                urn = dataContext.GetObjectUrn(),
                description = dataContext.GetDescription() + "_updated"
            };
            GeospatialManagementResponse responseUpdateObjectData;
            // call endpoint  
            actionResult = tester.UpdateObject(requestUpdateObjectData, out responseUpdateObjectData);
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUpdateObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Lookup Specific Object by URN");
            ObjectDataResponse responseObjectData;
            // call endpoint  
            actionResult = tester.LookupSpecificObjectByUrn(dataContext.GetObjectUrn(), out responseObjectData, dataContext.GetViewType());
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Lookup Object by Object URN");
            // call endpoint  
            actionResult = tester.LookupSpecificObjectByObjectUrn(responseNewObjectData.objectUrn, out responseObjectData, dataContext.GetViewType());
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "GeospatialManagementEndpoint", "Query Objects");
            // create request
            QueryObjectsRequest requestQueryObjectData = new QueryObjectsRequest
            {
                type = dataContext.GetType()
            };
            // call endpoint 
            QueryObjectsResponse responseQueryObjectsData;
            actionResult = tester.QueryObjects(requestQueryObjectData, out responseQueryObjectsData);
            result = result && (actionResult == GeospatialManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQueryObjectsData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}
