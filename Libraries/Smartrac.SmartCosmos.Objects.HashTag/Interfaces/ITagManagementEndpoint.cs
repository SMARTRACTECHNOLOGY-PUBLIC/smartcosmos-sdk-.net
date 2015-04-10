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
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.HashTag
{
    public enum HashTagActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed

        /// <summary>
        /// item already exists
        /// </summary>
        //Conflict
    }

    public interface IHashTagEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new Tag associated with the specified name
        /// </summary>
        /// <param name="requestData">Tag data</param>
        /// <param name="responseData">result</param>
        /// <returns>HashTagActionResult</returns>
        HashTagActionResult Create(HashTagRequest requestData, out HashTagResponse responseData);

        /// <summary>
        /// Lookup a specific Tag by its system-assigned URN key
        /// </summary>
        /// <param name="tagUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        HashTagActionResult Lookup(Urn tagUrn, out HashTagDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific Tag by their name
        /// </summary>
        /// <param name="tagName">Exact case-sensitive name to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        HashTagActionResult Lookup(string tagName, out HashTagDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Query all tags from a specific object
        /// </summary>
        /// <param name="tagName">Exact case-sensitive name to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        HashTagActionResult Lookup(EntityReferenceType entityReferenceType, Urn referenceUrn,
            out HashTagListResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Assign tags to objects
        /// </summary>
        /// <param name="tagName">Exact case-sensitive email address to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        HashTagActionResult Assign(EntityReferenceType entityReferenceType, Urn referenceUrn, HashTagListRequest requestData,
            out HashTagListResponse responseData);

        /// <summary>
        /// Deletes an existing tag by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        HashTagActionResult Delete(string tagName);
    }
}