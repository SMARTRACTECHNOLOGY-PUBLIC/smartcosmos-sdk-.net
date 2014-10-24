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

using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.RelationshipManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute(70)]
    public class TestCaseRelationshipManagementEndpoint : BaseObjectsTestCase
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
            RelationshipActionResult actionResult;

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
            actionResult = tester.Create(requestNewRelationshipData, out responseNewRelationshipData);
            result = result && (actionResult == RelationshipActionResult.Successful);
            // log response
            OnAfterTest(actionResult, responseNewRelationshipData);

            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Relationship Management by URN");
            RelationshipDataResponse responseLookupData;
            // call endpoint
            actionResult = tester.Lookup(responseNewRelationshipData.relationshipUrn,
                                                              out responseLookupData,
                                                              RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipActionResult.Successful);
            // log response
            OnAfterTest(actionResult, responseLookupData);

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
            actionResult = tester.Lookup(requestQueryData, out responseQueryData, RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipActionResult.Successful);
            // log response
            OnAfterTest(actionResult, responseQueryData);

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
            actionResult = tester.Lookup(requestQuerySpecificData, out responseQuerySpecificData, RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipActionResult.Successful);
            // log response
            OnAfterTest(actionResult, responseQuerySpecificData);

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
            actionResult = tester.Lookup(requestQueryTypeListData, out responseQueryTypeListData, RelationshipManagementDataContext.GetViewType());
            result = result && (actionResult == RelationshipActionResult.Successful);
            // log response
            OnAfterTest(actionResult, responseQueryTypeListData);

            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Deletes an existing relationship");
            // call endpoint
            actionResult = tester.Delete(responseNewRelationshipData.relationshipUrn);
            result = result && (actionResult == RelationshipActionResult.Successful);
            // log response
            OnAfterTest(actionResult);

            return result;
        }
    }
}