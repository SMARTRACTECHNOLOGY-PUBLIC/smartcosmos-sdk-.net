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

namespace Smartrac.SmartCosmos.Objects.ObjectInteraction
{
    public enum ObjectInteractionActionResult
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

    public interface IObjectInteractionEndpoint : IBaseEndpoint
    {
        /// <summaryObjectInteraction
        /// Capture a specific interaction
        /// </summaryObjectInteraction
        /// <param name="requestData"ObjectInteractionObject data</paramObjectInteraction
        /// <param name="responseData"ObjectInteraction result</paramObjectInteraction
        /// <returnsObjectInteractionObjectInteractionActionResult</returnsObjectInteraction
        ObjectInteractionActionResult CaptureObjectInteraction(CaptureObjectInteractionRequest requestData, out CaptureObjectInteractionResponse responseData);

        /// <summary>
        /// Lookup an array of matching interactions, If no objectUrn query parameter is included, then all interactions are returned.
        /// </summary>
        /// <param name="objectUrn">Optional: Developer-assigned objectUrn to search for</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectInteractionActionResult LookupMatchingInteractions(Urn objectUrn, out QueryObjectInteractionsResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific interaction by its system-assigned URN key
        /// </summary>
        /// <param name="interactionUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        ObjectInteractionActionResult LookupSpecificObjectInteractionbyURN(Urn interactionUrn, out QueryObjectInteractionsResponse responseData, ViewType viewType = ViewType.Standard);
    }
}
