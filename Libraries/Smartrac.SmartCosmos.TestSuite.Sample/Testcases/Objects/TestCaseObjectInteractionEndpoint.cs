using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.ObjectInteraction;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    [TestSuiteAttribute]
    public class TestCaseObjectInteractionEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IObjectInteractionDataContext dataContext = DataContextFactory.CreateObjectInteractionDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip ObjectInteractionEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IObjectInteractionEndpoint tester = EndpointFactory.CreateObjectInteractionEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateObjectInteractionEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            ObjectInteractionActionResult actionResult;

            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Capture a specific interaction");
            // create request         
            CaptureObjectInteractionRequest requestNewInteractionData = new CaptureObjectInteractionRequest
            {
                objectUrnObj = dataContext.GetObjectUrn(),
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetReferenceUrn(),
                type = dataContext.GetInteractionType(),
                recordedTimestamp = dataContext.GetRecordedTimestamp(),
            };
            CaptureObjectInteractionResponse responseNewInteractionData;
            // call endpoint  
            actionResult = tester.CaptureObjectInteraction(requestNewInteractionData, out responseNewInteractionData);
            result = result && (actionResult == ObjectInteractionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewInteractionData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Lookup Matching Interactions");
            QueryObjectInteractionsResponse responseLookupData;
            // call endpoint  
            actionResult = tester.LookupMatchingInteractions(dataContext.GetObjectUrn(),
                                                              out responseLookupData,
                                                              dataContext.GetViewType());
            result = result && (actionResult == ObjectInteractionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Lookup Specific Object Interaction by URN");
            // call endpoint  
            actionResult = tester.LookupSpecificObjectInteractionbyURN(responseNewInteractionData.interactionUrn,
                                                                        out responseLookupData,
                                                                        dataContext.GetViewType());
            result = result && (actionResult == ObjectInteractionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}
