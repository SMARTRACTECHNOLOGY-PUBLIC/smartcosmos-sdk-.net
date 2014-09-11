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
using System.Linq;
using System.Net;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
{
    public enum TagMetaDataActionResult
    {
        /// <summary>
        /// Minimum 1 tag found and result available
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed,

        /// <summary>
        /// user not authorized
        /// </summary>
        Unauthorized
    }

    public interface ITagMetadataEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Get tag related data
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagMetaDataActionResult</returns>
        TagMetaDataActionResult GetTagMetadata(TagMetaDataRequest requestData, out TagMetaDataResponse responseData);

        /// <summary>
        /// Get a message to a single tag code
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagMetaDataActionResult</returns>
        TagMetaDataActionResult GetTagMessage(TagMessageRequest requestData, out TagMessageResponse responseData);
    }
}
