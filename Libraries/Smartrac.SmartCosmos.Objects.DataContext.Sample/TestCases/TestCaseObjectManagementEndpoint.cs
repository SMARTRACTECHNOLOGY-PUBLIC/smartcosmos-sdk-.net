using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.ObjectManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseObjectManagementEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IObjectManagementDataContext dataContext = DataContextFactory.CreateObjectManagementDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip ObjectManagementEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IObjectManagementEndpoint tester = EndpointFactory.CreateObjectManagementEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateObjectManagementEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            ObjectManagementActionResult actionResult;

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Create new object");
            // create request         
            ObjectManagementNewRequest requestNewObjectData = new ObjectManagementNewRequest
            {
                urn = dataContext.GetObjectUrn(),
                type = dataContext.GetType(),
                name = dataContext.GetName(),
                description = dataContext.GetDescription(),
                activeFlag = dataContext.GetActiveFlag()
            };
            ObjectManagementResponse responseNewObjectData;
            // call endpoint  
            actionResult = tester.CreateNewObject(requestNewObjectData, out responseNewObjectData);
            result = result && (actionResult == ObjectManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Update an existing Object");
            // create request
            ObjectManagementRequest requestUpdateObjectData = new ObjectManagementRequest
            {
                urn = dataContext.GetObjectUrn(),
                description = dataContext.GetDescription() + "_updated"
            };
            ObjectManagementResponse responseUpdateObjectData;
            // call endpoint  
            actionResult = tester.UpdateObject(requestUpdateObjectData, out responseUpdateObjectData);
            result = result && (actionResult == ObjectManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUpdateObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Lookup Specific Object by URN");
            ObjectDataResponse responseObjectData;
            // call endpoint  
            actionResult = tester.LookupSpecificObjectByUrn(dataContext.GetObjectUrn(), out responseObjectData, dataContext.GetViewType());
            result = result && (actionResult == ObjectManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Lookup Object by Object URN");
            // call endpoint  
            actionResult = tester.LookupSpecificObjectByObjectUrn(responseNewObjectData.objectUrn, out responseObjectData, dataContext.GetViewType());
            result = result && (actionResult == ObjectManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Query Objects");
            // create request
            QueryObjectsRequest requestQueryObjectData = new QueryObjectsRequest
            {
                type = dataContext.GetType()
            };
            // call endpoint 
            QueryObjectsResponse responseQueryObjectsData;
            actionResult = tester.QueryObjects(requestQueryObjectData, out responseQueryObjectsData);
            result = result && (actionResult == ObjectManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQueryObjectsData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}
