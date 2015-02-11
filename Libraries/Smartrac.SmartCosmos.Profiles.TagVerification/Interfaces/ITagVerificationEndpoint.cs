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

namespace Smartrac.SmartCosmos.Profiles.TagVerification
{
    public enum TagVerificationActionResult
    {
        /// <summary>
        /// validation of import package was successful and data import will start immediately
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

    public interface ITagVerificationEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Verify tags for a verification type
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagVerificationActionResult</returns>
        TagVerificationActionResult VerifyTags(VerifyTagsRequest requestData, out VerifyTagsResponse responseData);

        /// <summary>
        /// Verify Tags for Round Rock compliance
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagVerificationActionResult</returns>
        TagVerificationActionResult VerifyTagsForRoundRockCompliance(VerifyTagsRequestRR requestData, out VerifyTagsResponse responseData);

        /// <summary>
        /// Get a message to a single verification state
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagVerificationActionResult</returns>
        TagVerificationActionResult GetVerificationMessage(VerificationMessageRequest requestData, out VerificationMessageResponse responseData);        
    }
}