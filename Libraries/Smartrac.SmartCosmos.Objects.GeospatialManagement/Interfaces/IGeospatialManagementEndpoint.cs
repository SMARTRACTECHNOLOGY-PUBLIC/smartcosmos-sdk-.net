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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    public enum GeospatialManagementActionResult
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

    public interface IGeospatialManagementEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new geospatial entry
        /// </summary>
        /// <param name="requestData">Geospatial data</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        GeospatialManagementActionResult CreateNewGeospatial(GeospatialManagementNewRequest requestData, out GeospatialManagementNewResponse responseData);

        /// <summary>
        /// Update an existing geospatial entry
        /// </summary>
        /// <param name="requestData">Geospatial data</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        GeospatialManagementActionResult UpdateGeospatial(GeospatialManagementUpdateRequest requestData, out GeospatialManagementUpdateResponse responseData);

        /// <summary>
        /// Lookup an array of matching geospatial entries. If no nameLike query parameter is included, then all interactions are returned.
        /// </summary>
        /// <param name="requestData">Geospatial data</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        GeospatialManagementActionResult LookupMatchingGeospatialEntries(QueryGeospatialEntriesRequest requestData, out QueryGeospatialEntriesResponse responseData);

        /// <summary>
        /// Lookup a specific geospatial entity by its system-assigned URN key
        /// </summary>
        /// <param name="geospatialUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        GeospatialManagementActionResult LookupSpecificGeospatialEntitybyURN(Urn geospatialUrn, out GeospatialEntryDataResponse responseData, ViewType viewType = ViewType.Standard);
    }
}
