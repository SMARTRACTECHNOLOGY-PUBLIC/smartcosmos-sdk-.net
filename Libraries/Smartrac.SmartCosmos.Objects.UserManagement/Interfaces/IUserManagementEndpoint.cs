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

namespace Smartrac.SmartCosmos.Objects.UserManagement
{
    public enum UserActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed,

        /// <summary>
        /// item already exists
        /// </summary>
        Conflict
    }

    public interface IUserManagementEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new user associated with the specified email address
        /// </summary>
        /// <param name="requestData">user data</param>
        /// <param name="responseData">result</param>
        /// <returns>UserManagementActionResult</returns>
        UserActionResult CreateNewUser(UserManagementRequest requestData, out UserManagementResponse responseData);

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="requestData">user data</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>UserManagementActionResult</returns>
        UserActionResult UpdateUser(UserManagementRequest requestData, out UserManagementResponse responseData);

        /// <summary>
        /// Lookup a specific user by its system-assigned URN key
        /// </summary>
        /// <param name="userUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">user data</param>
        /// <returns>UserManagementActionResult</returns>
        UserActionResult LookupSpecificUser(Urn userUrn, out UserDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific user by their email address
        /// </summary>
        /// <param name="eMailAddress">Exact case-sensitive email address to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">user data</param>
        /// <returns>UserManagementActionResult</returns>
        UserActionResult LookupSpecificUser(string eMailAddress, out UserDataResponse responseData, ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Initiate a reset password workflow or specifically define the user's password
        /// </summary>
        /// <param name="eMailAddress">eMailAddress</param>
        /// /// <param name="eMailAddress">optional: newPassword</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>UserManagementActionResult</returns>
        UserActionResult ChangeOrResetUserPassword(ChangeOrResetUserPasswordRequest requestData, out ChangeOrResetUserPasswordResponse responseData);
    }
}