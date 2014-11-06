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
using Smartrac.SmartCosmos.Objects.RelationshipManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(70)]
    public class TestCaseRelationshipManagementEndpoint : BaseTestCaseRelationshipManagementEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn relationshipUrn;

            return RunTestCase_Create(out relationshipUrn) &&
                RunTestCase_Lookup(relationshipUrn) &&
                RunTestCase_LookupAll() &&
                RunTestCase_LookupSpecific() &&
                RunTestCase_LookupSpecificType() &&
                RunTestCase_Delete(relationshipUrn);
        }

        protected virtual bool RunTestCase_Create(out Urn relationshipUrn)
        {
            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Create a new relationship");
            // create request
            RelationshipManagementRequest requestNewRelationshipData = new RelationshipManagementRequest
            {
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetReferenceUrn(),
                relatedEntityReferenceType = dataContext.GetRelatedEntityReferenceType(),
                relatedReferenceUrnObj = dataContext.GetRelatedReferenceUrn(),
                type = dataContext.GetRelationshipType(),
            };
            RelationshipManagementResponse responseNewRelationshipData;
            // call endpoint
            RelationshipActionResult actionResult = endpoint.Create(requestNewRelationshipData, out responseNewRelationshipData);
            OnAfterTest(actionResult);

            if (responseNewRelationshipData != null)
                relationshipUrn = responseNewRelationshipData.relationshipUrn;
            else
                relationshipUrn = null;

            return (actionResult == RelationshipActionResult.Successful);
        }

        protected virtual bool RunTestCase_Lookup(Urn relationshipUrn)
        {
            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup relationship by URN");
            RelationshipDataResponse responseLookupData;
            // call endpoint
            RelationshipActionResult actionResult = endpoint.Lookup(relationshipUrn,
                                                              out responseLookupData,
                                                              dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == RelationshipActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupAll()
        {
            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup All Relationships Between Entities");
            // create request
            QueryQueryRelationshipsRequest requestQueryData = new QueryQueryRelationshipsRequest
            {
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetReferenceUrn(),
                relatedEntityReferenceType = dataContext.GetRelatedEntityReferenceType(),
                relatedReferenceUrnObj = dataContext.GetRelatedReferenceUrn(),
            };
            QueryObjectRelationshipsResponse responseQueryData;
            // call endpoint
            RelationshipActionResult actionResult = endpoint.Lookup(requestQueryData, out responseQueryData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == RelationshipActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupSpecific()
        {
            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup Specific Relationship Between Entities");
            // create request
            QueryQueryRelationshipByTypeRequest requestQuerySpecificData = new QueryQueryRelationshipByTypeRequest
            {
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetReferenceUrn(),
                relatedEntityReferenceType = dataContext.GetRelatedEntityReferenceType(),
                relatedReferenceUrnObj = dataContext.GetRelatedReferenceUrn(),
                type = dataContext.GetRelationshipType()
            };
            RelationshipDataResponse responseQuerySpecificData;
            // call endpoint
            RelationshipActionResult actionResult = endpoint.Lookup(requestQuerySpecificData, out responseQuerySpecificData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == RelationshipActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupSpecificType()
        {
            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Lookup Specific Relationship Between Entities");
            // create request
            QueryQueryRelationshipsByTypeRequest requestQueryTypeListData = new QueryQueryRelationshipsByTypeRequest
            {
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetReferenceUrn(),
                type = dataContext.GetRelationshipType()
            };
            QueryObjectRelationshipsResponse responseQueryTypeListData;
            // call endpoint
            RelationshipActionResult actionResult = endpoint.Lookup(requestQueryTypeListData, out responseQueryTypeListData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == RelationshipActionResult.Successful);
        }

        protected virtual bool RunTestCase_Delete(Urn relationshipUrn)
        {
            OnBeforeTest("Objects", "RelationshipManagementEndpoint", "Deletes an existing relationship");
            // call endpoint
            RelationshipActionResult actionResult = endpoint.Delete(relationshipUrn);
            OnAfterTest(actionResult);
            return (actionResult == RelationshipActionResult.Successful);
        }
    }
}