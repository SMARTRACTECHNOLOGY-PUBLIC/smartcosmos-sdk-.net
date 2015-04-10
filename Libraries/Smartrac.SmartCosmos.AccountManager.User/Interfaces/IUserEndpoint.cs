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
using Smartrac.SmartCosmos.AccountManager.Base;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.AccountManager.User
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
        Failed

        /// <summary>
        /// item already exists
        /// </summary>
        //Conflict
    }

    public interface IUserEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Creates a user account and stores it.
        /// </summary>
        /// <param name="requestData">User data</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        UserActionResult Create(UserRequest requestData, out DefaultResponse responseData);

        /// <summary>
        /// Returns user's account information.
        /// </summary>
        /// <param name="userEmail">User email</param>
        /// <param name="responseData">User data</param>
        /// <param name="showRoles">Show or not user roles</param>
        /// <returns>UserActionResult</returns>
        UserActionResult Lookup(Email userEmail, out DefaultResponse responseData, bool showRoles = false);

        /// <summary>
        /// Send a reset password email.
        /// </summary>
        /// <param name="userEmail">User email</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        UserActionResult ResetLostPassword(Email userEmail, out DefaultResponse responseData);

        /// <summary>
        /// Updates user's account information.
        /// </summary>
        /// <param name="userData">User data</param>
        /// <param name="userEmail">email</param>
        /// <param name="responseData">result</param>
        /// <returns></returns>
        UserActionResult Update(UserUpdateRequest userData, Email userEmail, out DefaultResponse responseData);

        /// <summary>
        /// Assigns a role to a user, both within the same directory.
        /// </summary>
        /// <param name="userEmail">email</param>
        /// <param name="roleUrns">List of roles URNs</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        UserActionResult Assign(Email userEmail, List<UserRoleUrn> roleUrns, out UserRolesResponse responseData);

        /// <summary>
        /// Removes a role from a user, both within the same directory.
        /// </summary>
        /// <param name="userEmail">User email</param>
        /// <param name="roleUrn">Role URN</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        UserActionResult Dissociate(Email userEmail, Urn roleUrn, out DefaultResponse responseData);
    }
    
}
