using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.RelationshipManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseRelationshipManagementEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IRelationshipManagementDataContext RelationshipManagementDataContext = DataContextFactory.CreateRelationshipManagementDataContext();
            if (RelationshipManagementDataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip RelationshipManagementEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IRelationshipManagementEndpoint tester = EndpointFactory.CreateRelationshipManagementEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateRelationshipManagementEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            RelationshipManagementActionResult actionResult;


            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Create a new relationship");
            // create request         
            RelationshipManagementRequest requestNewRelationshipData = new RelationshipManagementRequest
            {
                entityReferenceType = RelationshipManagementDataContext.GetEntityReferenceType(),
                referenceUrnObj = RelationshipManagementDataContext.GetReferenceUrn(),
                relatedEntityReferenceType = RelationshipManagementDataContext.GetRelatedEntityReferenceType(),
                relatedReferenceUrnObj = RelationshipManagementDataContext.GetRelatedReferenceUrn(),
                type = RelationshipManagementDataContext.GetRelationshipType(),
            };
            RelationshipManagementResponse responseNewRelationshipData;
            // call endpoint  
            actionResult = tester.CreateNewRelationship(requestNewRelationshipData, out responseNewRelationshipData);
            result = result && (actionResult == RelationshipManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewRelationshipData.ToJSON());
            OnAfterTest();


            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Relationship Management by URN");
            RelationshipDataResponse responseLookupData;
            // call endpoint  
            actionResult = tester.LookupSpecificRelationshipByUrn(responseNewRelationshipData.relationshipUrn,
                                                              out responseLookupData,
                                                              RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();


            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup All Relationships Between Entities");
            // create request         
            QueryQueryRelationshipsRequest requestQueryData = new QueryQueryRelationshipsRequest
            {
                entityReferenceType = RelationshipManagementDataContext.GetEntityReferenceType(),
                referenceUrnObj = RelationshipManagementDataContext.GetReferenceUrn(),
                relatedEntityReferenceType = RelationshipManagementDataContext.GetRelatedEntityReferenceType(),
                relatedReferenceUrnObj = RelationshipManagementDataContext.GetRelatedReferenceUrn(),
            };
            QueryObjectRelationshipsResponse responseQueryData;
            // call endpoint  
            actionResult = tester.LookupAllRelationshipsBetweenEntities(requestQueryData, out responseQueryData, RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQueryData.ToJSON());
            OnAfterTest();


            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup Specific Relationship Between Entities");
            // create request         
            QueryQueryRelationshipByTypeRequest requestQuerySpecificData = new QueryQueryRelationshipByTypeRequest
            {
                entityReferenceType = RelationshipManagementDataContext.GetEntityReferenceType(),
                referenceUrnObj = RelationshipManagementDataContext.GetReferenceUrn(),
                relatedEntityReferenceType = RelationshipManagementDataContext.GetRelatedEntityReferenceType(),
                relatedReferenceUrnObj = RelationshipManagementDataContext.GetRelatedReferenceUrn(),
                type = RelationshipManagementDataContext.GetRelationshipType()
            };
            RelationshipDataResponse responseQuerySpecificData;
            // call endpoint  
            actionResult = tester.LookupSpecificRelationshipBetweenEntities(requestQuerySpecificData, out responseQuerySpecificData, RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQuerySpecificData.ToJSON());
            OnAfterTest();


            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup Specific Relationship Between Entities");
            // create request         
            QueryQueryRelationshipsByTypeRequest requestQueryTypeListData = new QueryQueryRelationshipsByTypeRequest
            {
                entityReferenceType = RelationshipManagementDataContext.GetEntityReferenceType(),
                referenceUrnObj = RelationshipManagementDataContext.GetReferenceUrn(),
                type = RelationshipManagementDataContext.GetRelationshipType()
            };
            QueryObjectRelationshipsResponse responseQueryTypeListData;
            // call endpoint  
            actionResult = tester.LookupAllRelationshipsByType(requestQueryTypeListData, out responseQueryTypeListData, RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQueryTypeListData.ToJSON());
            OnAfterTest();


            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Deletes an existing relationship");
            // call endpoint  
            actionResult = tester.RelationshipDeletion(responseNewRelationshipData.relationshipUrn);
            result = result && (actionResult == RelationshipManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            //Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}
