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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectInteractionSession
{
    public enum ObjInteractSessionActionResult
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

    public interface IObjectInteractionSessionEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Start a new object interaction session
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectInteractionSessionActionResult</returns>
        ObjInteractSessionActionResult Start(StartObjectInteractionSessionRequest requestData,
                                             out StartObjectInteractionSessionResponse responseData);

        /// <summary>
        /// Stop an existing object interaction session
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectInteractionSessionActionResult</returns>
        ObjInteractSessionActionResult Stop(StopObjectInteractionSessionRequest requestData,
                                            out StopObjectInteractionSessionResponse responseData);

        /// <summary>
        /// Stop an existing object interaction session
        /// </summary>
        /// <param name="sessionUrn">Object interaction session urn</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectInteractionSessionActionResult</returns>
        ObjInteractSessionActionResult Stop(Urn sessionUrn,
                                            out StopObjectInteractionSessionResponse responseData);

        /// <summary>
        /// Lookup a specific session by its system-assigned URN key
        /// </summary>
        /// <param name="sessionUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjInteractSessionActionResult Lookup(Urn sessionUrn,
                                                    out ObjectInteractionSessionDataResponse responseData,
                                                    ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific session by its system-assigned URN key
        /// </summary>
        /// <param name="interactionUrn">A case-sensitive starts with string pattern to match against. If omitted, (optional)</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjInteractSessionActionResult Lookup(string nameLike,
                                                    out ObjectInteractionSessionDataListResponse responseData,
                                                    ViewType viewType = ViewType.Standard);
    }
}