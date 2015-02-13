#region License

// SMART COSMOS .Net SDK
// (C) Copyright 2013 - 2015 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.RelationshipManagement
{
    public enum RelationshipActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed
    }

    public interface IRelationshipManagementEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new relationship
        /// </summary>
        /// <param name="requestData">Relationship data</param>
        /// <param name="responseData">result</param>
        /// <returns>RelationshipManagementActionResult</returns>
        RelationshipActionResult Create(RelationshipManagementRequest requestData, out RelationshipManagementResponse responseData);

        /// <summary>
        /// Lookup a specific relationship by its system-assigned URN key
        /// </summary>
        /// <param name="RelationshipUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Relationship data</param>
        /// <returns>RelationshipManagementActionResult</returns>
        RelationshipActionResult Lookup(Urn relationshipUrn, out RelationshipDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup an array of all defined relationships between any two entities
        /// </summary>
        /// <param name="requestData">filter data</param>
        /// <param name="responseData">list of relationships</param>
        /// <returns>RelationshipManagementActionResult</returns>
        RelationshipActionResult Lookup(QueryQueryRelationshipsRequest requestData, out QueryObjectRelationshipsResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific relationships between any two entities
        /// </summary>
        /// <param name="RelationshipUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Relationship data</param>
        /// <returns>RelationshipManagementActionResult</returns>
        RelationshipActionResult Lookup(QueryQueryRelationshipByTypeRequest requestData, out RelationshipDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup an array of all defined relationships of the specific type
        /// </summary>
        /// <param name="requestData">filter data</param>
        /// <param name="responseData">list of relationships</param>
        /// <returns>RelationshipManagementActionResult</returns>
        RelationshipActionResult Lookup(QueryQueryRelationshipsByTypeRequest requestData, out QueryObjectRelationshipsResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Deletes an existing relationship by its system-assigned URN key
        /// </summary>
        /// <param name="relationshipUrn">relationshipUrn of the relationship record</param>
        /// <returns>RelationshipManagementActionResult</returns>
        RelationshipActionResult Delete(Urn relationshipUrn);
    }
}