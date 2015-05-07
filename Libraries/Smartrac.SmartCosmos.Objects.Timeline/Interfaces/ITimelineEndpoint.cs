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
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Timeline
{
    public enum TimelineActionResult
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

    public interface ITimelineEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new Timeline associated with an arbitrary identification value
        /// </summary>
        /// <param name="requestData">Identification, name,  type</param>
        /// <param name="responseData">Code, urn</param>
        /// <returns>TimelineActionResult</returns>
        TimelineActionResult Create(TimelineDefinitionRequest requestData, out TimelineDefinitionResponse responseData);

        /// <summary>
        /// Update an existing Timeline
        /// </summary>
        /// <param name="requestData">Identification or urn must be used to identify of element</param>
        /// <param name="responseData">Code, message</param>
        /// <returns>TimelineActionResult</returns>
        TimelineActionResult Update(TimelineUpdateRequest requestData, out TimelineUpdateResponse responseData);

        /// <summary>
        /// Lookup a specific Timeline by its system-assigned URN key
        /// </summary>
        /// <param name="timelineUrn">Timeline urn</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>TimelineActionResult</returns>
        TimelineActionResult Lookup(Urn timelineUrn,
                                    out TimelineLookupResponse responseData,
                                    ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup an array of matching Timelines
        /// </summary>
        /// <param name="identificationElement">Name or part of it</param>
        /// <param name="responseData">Array of elements with all available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>TimelineActionResult</returns>
        TimelineActionResult Lookup(string nameLike,
                                    out TimelineLookupsResponse responseData,
                                    ViewType viewType = ViewType.Standard);
    }
}