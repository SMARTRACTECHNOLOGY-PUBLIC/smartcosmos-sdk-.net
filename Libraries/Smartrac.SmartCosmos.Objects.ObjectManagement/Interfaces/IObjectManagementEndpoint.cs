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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectManagement
{
    public enum ObjectManagementActionResult
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

    public interface IObjectManagementEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new Object associated with the specified email address
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectManagementActionResult CreateNewObject(ObjectManagementNewRequest requestData, out ObjectManagementResponse responseData);

        /// <summary>
        /// Update an existing Object
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectManagementActionResult UpdateObject(ObjectManagementRequest requestData, out ObjectManagementResponse responseData);

        /// <summary>
        /// Lookup a specific object by its system-assigned URN key
        /// </summary>
        /// <param name="urn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectManagementActionResult LookupSpecificObjectByUrn(Urn urn, out ObjectDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific object by their arbitrary developer assigned object URN
        /// </summary>
        /// <param name="objectUrn">Case-sensitive object URN to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="exact">Defaults to true; when false, a starts-with search is performed</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectManagementActionResult LookupSpecificObjectByObjectUrn(Urn objectUrn, out ObjectDataResponse responseData, ViewType viewType = ViewType.Standard, bool exact = true);

        /// <summary>
        /// Lookup an array of matching objects
        /// </summary>
        /// <param name="requestData">Object query request (e.g. filters like name, objectURN, ...)</param>
        /// <param name="responseData">List of objects</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectManagementActionResult QueryObjects(QueryObjectsRequest requestData, out QueryObjectsResponse responseData);
    }
}
